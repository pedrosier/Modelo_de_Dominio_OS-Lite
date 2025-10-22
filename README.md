# OS-Lite: Sistema de Gerenciamento de Ordens de Serviço

## 📋 Descrição

Este repositório contém a solução para o Trabalho Prático OS-Lite, focando no uso de classes, records, record structs e enums com navegabilidade bidirecional em um modelo de domínio para gerenciamento de ordens de serviço em uma assistência técnica. A implementação foi realizada por TDD (Red-Green-Refactor), garantindo invariantes como validações fail-fast, transições de status, consistência bidirecional e derivação de totais. O projeto enfatiza o domínio puro, com testes unitários cobrindo casos felizes, de falha e de borda. Restrições: sem herança, sem polimorfismo e sem associações N:M (apenas 1:1 e 1:N).

Disciplina: POO 4 2025/2.

### Justificativas de Modelagem

- **Classes para Entidades**: Usadas para `Cliente` e `OrdemDeServico` devido à semântica de identidade (entidades mutáveis com ciclo de vida, como IDs e coleções navegáveis). Classes permitem mutabilidade controlada e métodos para sincronizar associações bidirecionais, evitando estados inválidos.
- **Records/Record Structs para VOs**: `Money` como record struct para semântica de valor imutável (comparação por valor, não referência), promovendo imutabilidade e facilitando testes de igualdade. `Email` (opcional) como record para validação de formato, garantindo imutabilidade e refatorações seguras.
- **Enums**: Usados para `StatusOS` e `CategoriaItem` para representar estados finitos e explícitos, evitando "strings mágicas" e melhorando legibilidade, validação e transições de estado (ex.: guards em métodos como `IniciarExecucao`).

## 🚀 Tecnologias Utilizadas

- .NET 9.0
- C# 12
- xUnit

## 🏗️ Arquitetura

O projeto segue uma abordagem de domínio isolado, organizado em:

- **src/OSLite.Domain**: Entidades (ex.: `Cliente`, `OrdemDeServico`, `ItemDeServico`), value objects (ex.: `Money`), enums (ex.: `StatusOS`), serviços/métodos de domínio para operações (ex.: adicionar/remover itens, transições de status) e exceções personalizadas para invariantes.
- **tests/OSLite.Domain.Tests**: Suíte de testes unitários cobrindo o escopo funcional (ex.: criação de OS, adição de itens, fluxos de status, navegabilidade bidirecional) e invariantes (ex.: Money não negativo, quantidades >0, consistência de associações).

Não há infraestrutura externa (ex.: banco de dados); o foco é no domínio e nas regras de negócio.

## ⚙️ Como Executar

### Pré-requisitos

- .NET SDK 9.0+

### Clonar repositório

git clone https://github.com/pedrosier/Modelo_de_Dominio_OS-Lite

### Restaurar dependências

dotnet restore

### Executar testes

dotnet test tests/OSLite.Domain.Tests

Nota: Não há uma aplicação console ou web; o projeto é focado em domínio e testes. Para demonstração, execute os testes ou integre o domínio em um projeto maior.

## 👥 Aluno

Pedro Reis

## 📘 Informações da Disciplina

- **Curso:** Ciência da Computação
- **Disciplina:** Programação Orientada a Objetos
- **Orientador:** Prof. Dr. Everton Coimbra
