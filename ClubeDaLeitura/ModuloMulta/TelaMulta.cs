using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloAmigo;
using ClubeDaLeitura.ModuloEmprestimo;

namespace ClubeDaLeitura.ModuloMulta;

public class TelaMulta
{
    public RepositorioAmigo RepositorioAmigo;
    public RepositorioEmprestimo RepositorioEmprestimo;
    public RepositorioMulta RepositorioMulta;

    public TelaMulta(RepositorioAmigo repositorioAmigo, RepositorioEmprestimo repositorioEmprestimo, RepositorioMulta repositorioMulta)
    {
        RepositorioAmigo = repositorioAmigo;
        RepositorioEmprestimo = repositorioEmprestimo;
        RepositorioMulta = repositorioMulta;
    }
    public string ApresentarMenu()
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("1 >> Visualizar Multas Pendentes");
        ColorirEscrita.ComQuebraLinha("2 >> Pagar Multa");
        ColorirEscrita.ComQuebraLinha("3 >> Visualizar Multas de um Amigo");
        ColorirEscrita.ComQuebraLinha("S >> Voltar");

        ColorirEscrita.SemQuebraLinha("\nOpção: ");
        string opcao = Console.ReadLine()!;

        if (opcao == null)
            return null!;
        else
            return opcao.Trim().ToUpper();
    }
    public void ExibirCabecalho()
    {
        Console.Clear();
        ColorirEscrita.ComQuebraLinha("--------------------------------------------");
        ColorirEscrita.ComQuebraLinha("Gestão de Multas");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------\n");
    }
    public void MostrarMultasPendentes(bool exibirCabecalho, bool comId)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Visualizando Multas...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------\n");


        if (comId)
        {
            string[] cabecalho = ["Id", "Amigo", "Revista Emprestada", "Valor da Multa", "Status"];
            int[] espacamentos = [6, 20, 30, 20, 20];
            ConsoleColor[] coresCabecalho = [ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.White];

            ColorirEscrita.PintarCabecalho(cabecalho, espacamentos, coresCabecalho);
        }
        else
        {
            string[] cabecalho = ["Amigo", "Revista Emprestada", "Valor da Multa", "Status"];
            int[] espacamentos = [20, 30, 20, 20];
            ConsoleColor[] coresCabecalho = [ConsoleColor.Cyan, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.White];

            ColorirEscrita.PintarCabecalho(cabecalho, espacamentos, coresCabecalho);
        }

        RepositorioEmprestimo.VerificarEmprestimosAtrasados(RepositorioEmprestimo.Emprestimos);

        foreach (Emprestimo e in RepositorioEmprestimo.Emprestimos)
        {
            if (e == null)
                continue;

            if (e.Situacao == "ATRASADO" && !e.Amigo.Multas.Any(m => m != null && m.Emprestimo.Id == e.Id))
            {
                Multa novaMulta = new Multa(e);
                RepositorioMulta.RegistrarMulta(novaMulta);
                e.Amigo.ReceberMulta(novaMulta);
            }
        }

        Multa[] multasPendentes = RepositorioMulta.PegarListaMultasPendentes();

        int quantidadeMultas = 0;

        for (int i = 0; i < multasPendentes.Length; i++)
        {
            Multa m = multasPendentes[i];

            if (m == null)
                continue;

            quantidadeMultas++;
            RepositorioMulta.ListaVazia = false;

            if (comId)
            {
                string[] cabecalho = [m.Id.ToString(), m.Emprestimo.Amigo.Nome, m.Emprestimo.Revista.Titulo, m.ValorMulta.ToString(), m.Status];
                int[] espacamentos = [6, 20, 30, 20, 20];
                ConsoleColor[] coresCabecalho = [ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.White];

                ColorirEscrita.PintarLinha(cabecalho, espacamentos, coresCabecalho);
            }
            else
            {
                string[] cabecalho = [m.Emprestimo.Amigo.Nome, m.Emprestimo.Revista.Titulo, m.ValorMulta.ToString(), m.Status];
                int[] espacamentos = [20, 30, 20, 20];
                ConsoleColor[] coresCabecalho = [ConsoleColor.Cyan, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.White];

                ColorirEscrita.PintarLinha(cabecalho, espacamentos, coresCabecalho);
            }
        }

        if (quantidadeMultas == 0)
        {
            Notificador.ExibirMensagem("\nNenhuma multa no histórico!", ConsoleColor.Red);
            RepositorioMulta.ListaVazia = true;
        }
    }
    public void MostrarMultasAmigo(bool exibirCabecalho, bool comId)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        MostrarListaAmigos(false, true);

        if (RepositorioAmigo.ListaVazia)
            return;

        bool idAmigoValido;
        int idAmigoEscolhido;

        do
        {
            ColorirEscrita.ComQuebraLinha("\n--------------------------------------------");
            ColorirEscrita.SemQuebraLinha("Selecione o ID de um Amigo: ");
            idAmigoValido = int.TryParse(Console.ReadLine(), out idAmigoEscolhido);

            if (!idAmigoValido)
            {
                Notificador.ExibirMensagem("\nO ID selecionado é inválido!", ConsoleColor.Red);
                return;
            }
        } while (!idAmigoValido);

        Amigo amigoEscolhido = RepositorioAmigo.SelecionarPorId(idAmigoEscolhido);

        RepositorioEmprestimo.VerificarEmprestimosAtrasados(RepositorioEmprestimo.Emprestimos);

        foreach (Emprestimo e in amigoEscolhido.Emprestimos)
        {
            if (e == null)
                continue;

            if (e.Situacao == "ATRASADO" && !e.Amigo.Multas.Any(m => m != null && m.Emprestimo.Id == e.Id))
            {
                Multa novaMulta = new Multa(e);
                RepositorioMulta.RegistrarMulta(novaMulta);
                e.Amigo.ReceberMulta(novaMulta);
            }
        }

        Multa[] multasPendentesAmigo = amigoEscolhido.ObterMultas();

        if (exibirCabecalho)
            ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha($"Visualizando Multas de {amigoEscolhido.Nome}...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------\n");

        if (multasPendentesAmigo.All(m => m == null))
        {
            Notificador.ExibirMensagem($"O {amigoEscolhido.Nome} não tem multas no histórico.", ConsoleColor.Red);
            return;
        }

        if (comId)
        {
            string[] cabecalho = ["Id", "Revista Emprestada", "Valor da Multa", "Status"];
            int[] espacamentos = [6, 30, 20, 20];
            ConsoleColor[] coresCabecalho = [ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.White];

            ColorirEscrita.PintarCabecalho(cabecalho, espacamentos, coresCabecalho);
        }
        else
        {
            string[] cabecalho = ["Revista Emprestada", "Valor da Multa", "Status"];
            int[] espacamentos = [30, 20, 20];
            ConsoleColor[] coresCabecalho = [ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.White];

            ColorirEscrita.PintarCabecalho(cabecalho, espacamentos, coresCabecalho);
        }

        foreach (Multa m in multasPendentesAmigo)
        {
            if (m == null)
                continue;

            if (comId)
            {
                string[] cabecalho = [m.Id.ToString(), m.Emprestimo.Revista.Titulo, m.ValorMulta.ToString(), m.Status];
                int[] espacamentos = [6, 30, 20, 20];
                ConsoleColor[] coresCabecalho = [ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.White];

                ColorirEscrita.PintarLinha(cabecalho, espacamentos, coresCabecalho);
            }
            else
            {
                string[] cabecalho = [m.Emprestimo.Revista.Titulo, m.ValorMulta.ToString(), m.Status];
                int[] espacamentos = [30, 20, 20];
                ConsoleColor[] coresCabecalho = [ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.White];

                ColorirEscrita.PintarLinha(cabecalho, espacamentos, coresCabecalho);
            }
        }
    }
    public void MostrarListaAmigos(bool exibirCabecalho, bool comId)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Visualizando Amigos...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------\n");

        if (comId)
        {
            string[] cabecalho = ["Id", "Nome", "Responsável", "Telefone"];
            int[] espacamentos = [6, 20, 20, 20];
            ConsoleColor[] coresCabecalho = [ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Cyan, ConsoleColor.Blue];

            ColorirEscrita.PintarCabecalho(cabecalho, espacamentos, coresCabecalho);
        }
        else
        {
            string[] cabecalho = ["Nome", "Responsável", "Telefone"];
            int[] espacamentos = [20, 20, 20];
            ConsoleColor[] coresCabecalho = [ConsoleColor.Cyan, ConsoleColor.Cyan, ConsoleColor.Blue];

            ColorirEscrita.PintarCabecalho(cabecalho, espacamentos, coresCabecalho);
        }

        Amigo[] amigosRegistrados = RepositorioAmigo.PegarListaRegistrados();

        int quantidadeAmigos = 0;

        for (int i = 0; i < amigosRegistrados.Length; i++)
        {
            Amigo a = amigosRegistrados[i];

            if (a == null)
                continue;

            quantidadeAmigos++;
            RepositorioAmigo.ListaVazia = false;

            if (comId)
            {
                string[] linha = [a.Id.ToString(), a.Nome, a.Responsavel, a.Telefone];
                int[] espacamentos = [6, 20, 20, 20];
                ConsoleColor[] coresLinha = [ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Cyan, ConsoleColor.Blue];

                ColorirEscrita.PintarLinha(linha, espacamentos, coresLinha);
            }
            else
            {
                string[] linha = [a.Nome, a.Responsavel, a.Telefone];
                int[] espacamentos = [20, 20, 20];
                ConsoleColor[] coresLinha = [ConsoleColor.Cyan, ConsoleColor.Cyan, ConsoleColor.Blue];

                ColorirEscrita.PintarLinha(linha, espacamentos, coresLinha);
            }
        }

        if (quantidadeAmigos == 0)
        {
            Notificador.ExibirMensagem("\nNenhum amigo registrado!", ConsoleColor.Red);
            RepositorioAmigo.ListaVazia = true;
        }
    }
    public void PagarMulta()
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Pagamento de Multas...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------");

        MostrarMultasPendentes(true, true);

        if (RepositorioMulta.ListaVazia)
            return;

        bool idValido;
        int idMultaEscolhida;

        do
        {
            ColorirEscrita.ComQuebraLinha("\n--------------------------------------------");
            ColorirEscrita.SemQuebraLinha("Selecione o ID de uma Multa: ");
            idValido = int.TryParse(Console.ReadLine(), out idMultaEscolhida);

            if (!idValido)
                Notificador.ExibirMensagem("\nO ID selecionado é inválido!", ConsoleColor.Red);
        } while (!idValido);

        Multa multaEscolhida = RepositorioMulta.SelecionarPorId(idMultaEscolhida);

        if (multaEscolhida == null)
        {
            Notificador.ExibirMensagem("\nO ID escolhido não está registrado.", ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            PagarMulta();
            return;
        }

        if (RepositorioMulta.VerificarMultaQuitada(multaEscolhida))
        {
            Notificador.ExibirMensagem("\nA multa escolhida já está quitada!", ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            PagarMulta();
        }

        multaEscolhida.PagarMulta();

        Notificador.ExibirMensagem("\nMulta paga com sucesso!", ConsoleColor.Green);
    }
}
