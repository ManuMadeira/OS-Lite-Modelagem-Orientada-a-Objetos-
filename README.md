# OS-Lite-Modelagem-Orientada-a-Objetos-
OS-Lite: Modelagem Orientada a Objetos com Classes/Records/Structs e Enums (1:N, TDD, Invariantes)

# OS-Lite - Sistema de Gestão de Ordens de Serviço

**Desenvolvido por: Emmanuelly Madeira**

## 📋 Sobre o Projeto

Implementação de um sistema de gestão para assistência técnica, seguindo os princípios de Domain-Driven Design (DDD) com foco em modelagem orientada a objetos, invariantes e testes.

### 🎯 Objetivos Atendidos

- ✅ **Modelagem adequada de tipos** (class × record/record struct × enum)
- ✅ **Invariantes e exceções claras** (fail-fast approach)
- ✅ **Associações 1:N e fluxo de status controlado**
- ✅ **Navegabilidade bidirecional consistente**
- ✅ **Testes TDD completos** (casos felizes e de falha)

## 🏗️ Estrutura do Projeto

```
OSLite/
├── OSLite.sln
├── OSLite.Domain/                 # Camada de Domínio
│   ├── Entities/
│   │   ├── Cliente.cs            # Entidade com identidade
│   │   ├── OrdemDeService.cs     # Entidade com navegabilidade bidirecional
│   │   └── ItemDeService.cs      # Objeto de composição
│   ├── ValueObjects/
│   │   ├── Money.cs              # Record struct imutável
│   │   └── Email.cs              # Record struct com validação
│   └── Enums/
│       ├── StatusOS.cs           # Estados explícitos da OS
│       └── CategoriaItem.cs      # Categorias de serviços
├── OSLite.Domain.Tests/          # Projeto de Testes
│   ├── ValueObjects/
│   ├── Entities/
│   └── BidirecionalTests.cs
└── OSLite.Console/               # Demonstração do sistema
    └── Program.cs
```

## 🧩 Decisões de Modelagem

### 🔷 **Classes vs Records vs Structs**

| Tipo | Uso | Justificativa |
|------|-----|---------------|
| **Class** | `Cliente`, `OrdemDeService`, `ItemDeService` | Entidades com identidade e ciclo de vida |
| **Record Struct** | `Money`, `Email` | Value Objects imutáveis com semântica de valor |
| **Enum** | `StatusOS`, `CategoriaItem` | Estados e categorias fixas com validação implícita |

### 🔗 **Associações Implementadas**

1. **Cliente ↔ OrdemDeService (1:N Bidirecional)**
   - Um cliente tem múltiplas ordens de serviço
   - Cada ordem pertence a exatamente um cliente
   - Navegabilidade sincronizada automaticamente

2. **OrdemDeService → ItensDeService (1:N Composição)**
   - Itens nascem e morrem com a ordem
   - Coleção encapsulada com operações controladas

## 🛡️ Invariantes e Validações

### **Money**
- ✅ Valor não negativo
- ✅ Operações matemáticas type-safe

### **ItemDeService**
- ✅ Descrição não vazia
- ✅ Quantidade > 0
- ✅ Subtotal calculado

### **OrdemDeService**
- ✅ Fluxo de status controlado
- ✅ Transições válidas com pré-condições
- ✅ Total derivado dos itens
- ✅ Bidirecionalidade consistente

### **Cliente**
- ✅ Nome não vazio
- ✅ Email válido (formato básico)

## 🔄 Fluxo de Status da OS

```mermaid
Aberta → EmExecucao (requer ≥1 item)
EmExecucao → Concluida
Aberta/EmExecucao → Cancelada
```

**Restrições:**
- ❌ Não adicionar/remover itens em `Concluida` ou `Cancelada`
- ❌ Não iniciar execução sem itens

## 🧪 Testes Implementados

### **Value Objects**
- `Money_nao_aceita_negativo`
- `Money_cria_valido_com_valor_nao_negativo`
- `Money_soma_corretamente`

### **Entities**
- `ItemDeService_cria_valido_e_calcula_subtotal`
- `ItemDeService_nao_cria_com_descricao_vazia`
- `OS_total_soma_subtotais_itens`
- `OS_aberta_inicia_execucao_quando_tem_itens`
- `OS_aberta_nao_inicia_sem_itens`
- `OS_nao_adiciona_itens_em_concluida_ou_cancelada`
- `OS_fluxo_aberta_para_execucao_para_concluida`

### **Bidirecionalidade**
- `Cliente_adiciona_ordem_sincroniza_cliente_na_ordem`
- `OS_trocar_de_cliente_atualiza_colecoes_dos_clientes`

## 🚀 Como Executar

### **Pré-requisitos**
- .NET 8.0 SDK ou superior

### **Comandos**
```bash
# Restaurar dependências
dotnet restore

# Executar testes
dotnet test

# Executar demonstração
dotnet run --project OSLite.Console
```

### **Build e Teste**
```bash
# Build completo
dotnet build

# Testes com output detalhado
dotnet test --logger "console;verbosity=normal"
```

## 📊 Funcionalidades do MVP

- [x] Abrir OS para cliente existente
- [x] Adicionar/Remover itens (apenas em Aberta/EmExecucao)
- [x] Iniciar execução (requer ≥1 item)
- [x] Concluir OS
- [x] Cancelar OS
- [x] Cálculo automático do total
- [x] Navegabilidade bidirecional
- [x] Validações fail-fast

## 🎯 Princípios Aplicados

### **Encapsulamento**
- Coleções expostas como `ReadOnlyCollection<T>`
- Mutação apenas via métodos da entidade
- Estados internos protegidos

### **Imutabilidade**
- Value Objects como record structs
- Propriedades somente leitura onde aplicável
- Operações que retornam novos objetos

### **Fail-Fast**
- Validações nos construtores
- Exceções específicas para regras violadas
- Mensagens de erro claras em português

## 🔮 Extensões Futuras

- [ ] VO Telefone com normalização
- [ ] VO DescricaoDeItem com limites
- [ ] Relatórios textuais
- [ ] Persistência em banco de dados
- [ ] API REST

## 📝 Reflexão Final - Emmanuelly Madeira

Ao desenvolver o OS-Lite, percebi como **records/structs e enums** transformam não apenas o código, mas toda a experiência de desenvolvimento. Os **record structs** para Value Objects como `Money` trouxeram imutabilidade nativa, eliminando efeitos colaterais e simplificando testes - dois `Money` com mesmo valor são naturalmente iguais, sem necessidade de complexas comparações.

Os **enums** como `StatusOS` substituíram "strings mágicas" por estados explícitos e type-safe, onde o próprio compilação previne estados inválidos. Isso tornou o código mais expressivo - `StatusOS.EmExecucao` comunica claramente a intenção, diferente de um misterioso `"em_execucao"`.

A abordagem **fail-fast com invariantes** revelou-se surpreendentemente poderosa no TDD. Cada validação no construtor criou um teste de falha natural e objetivo, transformando regras de negócio em especificações executáveis. A navegabilidade bidirecional, embora desafiadora, mostrou como manter consistência entre relacionamentos sem sacrificar encapsulamento.

No final, compreendi que essas escolhas não são apenas técnicas - são ferramentas que tornam o domínio mais tangível, o código mais comunicativo e a manutenção mais previsível. Cada `record struct`, cada `enum` e cada invariante são peças de um sistema que não apenas funciona, mas se explica e se protege.

---

**Desenvolvido por Emmanuelly Madeira**