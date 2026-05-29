# 🔍 CodeScanAI

> Analiza repositorios de código y genera resúmenes inteligentes usando IA — pensado para developers que necesitan entender un codebase rápidamente.

---

## ¿Qué hace?

**CodeScanAI** escanea la estructura y el contenido de un repositorio de código fuente y produce un briefing claro generado por IA. En lugar de leer archivos uno por uno, obtienes un resumen accionable: qué hace el proyecto, cómo está organizado y qué tecnologías utiliza.

---

## ✨ Funcionalidades

- 🧠 Análisis de código fuente asistido por IA
- 📦 Escaneo automático de la estructura de repositorios
- 📝 Generación de resúmenes / briefings legibles
- ⚙️ Integración con el ecosistema .NET y la JVM
- 🔗 Procesamiento a nivel de IR con LLVM para análisis profundo del código

---

## 🛠️ Stack tecnológico

| Capa | Tecnología |
|------|-----------|
| Lógica principal | C# · .NET MAUI |
| Análisis de código | LLVM (IR / bitcode) |
| Procesamiento JVM | Java |
| Entorno de desarrollo | Visual Studio |
| IA | Anthropic Claude API |
| Control de versiones | Git · GitHub |

---

## 📁 Estructura del proyecto

```
CodeScanAI/
├── CodeScanAI/        # Proyecto principal (.NET / C#)
├── CodeScanAI.slnx    # Solución Visual Studio
└── .gitignore
```

---

## 🚀 Cómo ejecutarlo localmente

### Prerrequisitos

- [.NET SDK 8+](https://dotnet.microsoft.com/download)
- [Java JDK 17+](https://adoptium.net/)
- [LLVM](https://llvm.org/) (para análisis de IR)
- Una API key de Anthropic ([obtener aquí](https://console.anthropic.com/))

### Instalación

```bash
# Clonar el repositorio
git clone https://github.com/dhontavo/CodeScanAI.git
cd CodeScanAI
```

Abre `CodeScanAI.slnx` con Visual Studio o ejecuta desde la terminal:

```bash
cd CodeScanAI
dotnet restore
dotnet run
```

### Variables de entorno

```env
ANTHROPIC_API_KEY=tu_api_key_aquí
```

---

## 💡 Contexto del proyecto

Este proyecto explora cómo combinar análisis estático de código — usando LLVM para trabajar con una representación intermedia del código independiente del lenguaje — con inteligencia artificial generativa para producir explicaciones comprensibles de cualquier codebase.

La integración de tres ecosistemas distintos (.NET, JVM y LLVM) fue el mayor reto técnico: lograr que cada capa se comunique de forma coherente y que la IA reciba el contexto necesario para generar resúmenes útiles.

---

## 👨‍💻 Autor

**dhontavo** — [github.com/dhontavo](https://github.com/dhontavo)

---
