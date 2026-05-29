using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CodeScanAI.Services;

public class AnthropicService : IAiService
{
    private readonly HttpClient _http;
    private const string Url = "https://api.anthropic.com/v1/messages";

    private readonly Dictionary<string, string> _prompts = new()
    {
        ["explain"] = "Explica este codigo en lenguaje simple y claro, como si se lo explicaras a alguien que esta aprendiendo:",
        ["bugs"] = "Analiza este codigo, detecta posibles bugs, errores o malas practicas y sugiere correcciones concretas:",
        ["docs"] = "Genera comentarios de documentacion profesionales (XML Doc para C# o JSDoc para JS/TS) para este codigo:",
        ["optimize"] = "Sugiere como optimizar este codigo en terminos de rendimiento, legibilidad y buenas practicas:"
    };

    public AnthropicService()
    {
        _http = new HttpClient();

        var apiKey = Environment.GetEnvironmentVariable("ANTHROPIC_API_KEY") 
                  ?? Environment.GetEnvironmentVariable("OPENAI_API_KEY");

        if (string.IsNullOrWhiteSpace(apiKey))
            throw new Exception("Anthropic API key not found. Set the ANTHROPIC_API_KEY environment variable.");

        _http.DefaultRequestHeaders.Add("x-api-key", apiKey);
        _http.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");
    }

    public async Task<string> AnalyzeAsync(string code, string analysisType)
    {
        var prompt = _prompts.GetValueOrDefault(analysisType, _prompts["explain"]);

        var body = new
        {
            model = "claude-3-5-sonnet-20241022",
            max_tokens = 1000,
            system = "Eres un experto desarrollador de software. Responde siempre en español, de forma clara y estructurada.",
            messages = new[]
            {
                new { role = "user", content = $"{prompt}\n\n```\n{code}\n```" }
            }
        };

        var json = JsonSerializer.Serialize(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _http.PostAsync(Url, content);

        if (!response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new Exception($"Error Anthropic: Unauthorized - check your API key. Response: {responseBody}");

            throw new Exception($"Error Anthropic: {response.StatusCode} - {responseBody}");
        }

        var result = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(result);
        return doc.RootElement
            .GetProperty("content")[0]
            .GetProperty("text")
            .GetString() ?? "Sin respuesta";
    }
}
