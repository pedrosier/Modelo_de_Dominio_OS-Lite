Reflexão (10-15 linhas)

Usei classes para entidades que representam identidade e ciclo de vida (Cliente, OrdemDeServico, ItemDeServico).
VOs (Money, Email) foram implementados como record/record struct para garantir imutabilidade e comparação por valor,
facilitando testes e evitando efeitos colaterais. Money é record struct para eficiência e semântica de valor pequeno.
Enums (StatusOS, CategoriaItem) tornam as transições e categorias explícitas, evitando strings mágicas.
As invariantes são protegidas com validações imediatas (fail-fast), o que simplifica o TDD: testes podem assertar
exceções específicas quando regras são violadas. A navegabilidade bidirecional foi concentrada em pontos internos
(de criação e em ChangeCliente) para manter consistência entre ClienteId, referência Cliente e coleções, evitando
estados intermediários inválidos. Em suma, separar identidade e valor melhora segurança do domínio, leitura e testes.
