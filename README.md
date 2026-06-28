Application d’IA conversationnelle développée en .NET 10, conçue pour offrir un assistant interne capable de répondre aux questions métiers, de générer des rapports, d’automatiser des tâches et de s’intégrer à n’importe quel système d’information.

Elle expose une API simple, fiable et sécurisée, permettant de consommer un LLM de manière contrôlée et industrialisée.

Le modèle utilisé est Mistral‑Large‑3, déployé sur Microsoft / Azure Foundry.

L’application est pensée pour être cloud‑native, scalable, sécurisée et maintenable, grâce à une architecture combinant Clean Architecture et Domain‑Driven Design (DDD).

Stack Technique: 

Développement: 
- .NET 10 Web API
- Clean Architecture
- Domain‑Driven Design (DDD)

IA & Modèles: 
- Azure OpenAI / Foundry
- Modèle LLM déployé : Mistral‑Large‑3

Cloud Azure: 
- Azure Container Apps (Dev & Prod)
- Azure Container Registry
- Azure Key Vault
- Azure DevOps Pipelines multi‑stages (Build / Dev / Prod)
- Variables d’environnement injectées automatiquement
- Docker via Dockerfile

Sécurité: 
- Secrets dans Azure Key Vault
- Accès via Managed Identity
- Aucun secret dans le code
