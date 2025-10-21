using OSLite.Domain.Entities;
using OSLite.Domain.Enums;
using OSLite.Domain.ValueObjects;

namespace OSLite.Console;

class Program
{
    static void Main(string[] args)
    {
        System.Console.WriteLine("🚀 OS-Lite - Sistema de Assistência Técnica");
        System.Console.WriteLine("===========================================\n");

        DemonstrarSistema();
        
        System.Console.WriteLine("\n✅ Demonstração concluída!");
        System.Console.WriteLine("Pressione qualquer tecla para sair...");
        System.Console.ReadKey();
    }

    static void DemonstrarSistema()
    {
        try
        {
            // 1. Criar clientes
            System.Console.WriteLine("1. 📋 CRIANDO CLIENTES");
            var cliente1 = new Cliente(1, "João Silva", new Email("joao@email.com"), "11999999999");
            var cliente2 = new Cliente(2, "Maria Santos", new Email("maria@email.com"), "11888888888");
            System.Console.WriteLine($"   ✅ Cliente 1: {cliente1.Nome} ({cliente1.Email})");
            System.Console.WriteLine($"   ✅ Cliente 2: {cliente2.Nome} ({cliente2.Email})");
            System.Console.WriteLine();

            // 2. Criar ordem de serviço
            System.Console.WriteLine("2. 📝 ABRINDO ORDEM DE SERVIÇO");
            var ordem = OrdemDeService.Criar(1, cliente1, DateOnly.FromDateTime(DateTime.Now));
            System.Console.WriteLine($"   ✅ OS #{ordem.Id} criada para {ordem.Cliente.Nome}");
            System.Console.WriteLine($"   📅 Data: {ordem.DataAbertura}");
            System.Console.WriteLine($"   🟢 Status: {ordem.Status}");
            System.Console.WriteLine();

            // 3. Adicionar itens à OS
            System.Console.WriteLine("3. 🔧 ADICIONANDO ITENS À OS");
            var item1 = new ItemDeService("Troca de tela", 1, new Money(250), CategoriaItem.Pecas);
            var item2 = new ItemDeService("Diagnóstico", 1, new Money(50), CategoriaItem.Diagnostico);
            var item3 = new ItemDeService("Mão de obra", 2, new Money(80), CategoriaItem.MaoDeObra);
            
            ordem.AdicionarItem(item1);
            System.Console.WriteLine($"   ✅ {item1.Descricao} - {item1.PrecoUnitario}");
            
            ordem.AdicionarItem(item2);
            System.Console.WriteLine($"   ✅ {item2.Descricao} - {item2.PrecoUnitario}");
            
            ordem.AdicionarItem(item3);
            System.Console.WriteLine($"   ✅ {item3.Descricao} - {item3.PrecoUnitario}");
            
            System.Console.WriteLine($"   💰 Total da OS: {ordem.Total}");
            System.Console.WriteLine();

            // 4. Iniciar execução
            System.Console.WriteLine("4. ⚡ INICIANDO EXECUÇÃO DA OS");
            ordem.IniciarExecucao();
            System.Console.WriteLine($"   🔄 Status: {ordem.Status}");
            System.Console.WriteLine();

            // 5. Demonstrar navegabilidade bidirecional
            System.Console.WriteLine("5. 🔄 NAVEGABILIDADE BIDIRECIONAL");
            System.Console.WriteLine($"   👤 Cliente {cliente1.Nome} tem {cliente1.Ordens.Count} OS(s)");
            System.Console.WriteLine($"   📋 OS #{ordem.Id} pertence a {ordem.Cliente.Nome}");
            System.Console.WriteLine();

            // 6. Trocar cliente (demonstrar bidirecionalidade)
            System.Console.WriteLine("6. 🔀 TROCANDO CLIENTE DA OS");
            ordem.TrocarCliente(cliente2);
            System.Console.WriteLine($"   🔄 OS transferida para {cliente2.Nome}");
            System.Console.WriteLine($"   👤 {cliente1.Nome} agora tem {cliente1.Ordens.Count} OS(s)");
            System.Console.WriteLine($"   👤 {cliente2.Nome} agora tem {cliente2.Ordens.Count} OS(s)");
            System.Console.WriteLine();

            // 7. Concluir OS
            System.Console.WriteLine("7. ✅ CONCLUINDO OS");
            ordem.Concluir();
            System.Console.WriteLine($"   🏁 Status: {ordem.Status}");
            System.Console.WriteLine($"   💰 Valor final: {ordem.Total}");
            System.Console.WriteLine();

            // 8. Demonstrar validações (tentativa de adicionar item em OS concluída)
            System.Console.WriteLine("8. 🛡️ TESTANDO VALIDAÇÕES");
            try
            {
                var itemExtra = new ItemDeService("Item extra", 1, new Money(100));
                ordem.AdicionarItem(itemExtra);
            }
            catch (InvalidOperationException ex)
            {
                System.Console.WriteLine($"   ❌ Validação funcionando: {ex.Message}");
            }

        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"💥 ERRO: {ex.Message}");
        }
    }
}