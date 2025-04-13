namespace ClubeDaLeitura.ModuloCaixa;

public class TelaCaixa
{
    public RepositorioCaixa RepositorioCaixa;

    public TelaCaixa(RepositorioCaixa repositorioCaixa)
    {
        RepositorioCaixa = repositorioCaixa;
    }
    public string ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine("1 >> Registrar Caixa");
        Console.WriteLine("2 >> Visualizar Lista de Caixas");
        Console.WriteLine("3 >> Editar Caixa");
        Console.WriteLine("4 >> Excluir Caixa");
        Console.WriteLine("S >> Voltar");

        Console.Write("\nOpção: ");

        return Console.ReadLine()!.ToUpper();
    }
    public void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("Gestão de Caixas");
        Console.WriteLine("--------------------------------------------\n");
    }
    public void RegistrarCaixa()
    {
        ExibirCabecalho();

        Console.WriteLine("Registrando Caixa...");
        Console.WriteLine("--------------------------------------------\n");

        Caixa novaCaixa = ObterDadosCaixa();

        string erros = novaCaixa.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine(erros);
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            RegistrarCaixa();
            return;
        }

        if (RepositorioCaixa.VerificarEtiquetas(novaCaixa))
        {
            Console.WriteLine("\nJá existe uma caixa com essa etiqueta!");
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            RegistrarCaixa();
            return;
        }

        RepositorioCaixa.RegistrarCaixa(novaCaixa);

        Console.WriteLine("\nCaixa registrada com sucesso!");
    }
    public void MostrarListaRegistrados(bool exibirCabecalho, bool comId)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        Console.WriteLine("Visualizando Caixas...");
        Console.WriteLine("--------------------------------------------\n");

        if (comId)
            Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -20} | {3, -20}",
                "Id", "Etiqueta", "Dias de Empréstimo", "Revistas na Caixa");
        else
            Console.WriteLine(
                "{0, -20} | {1, -20} | {2, -20}",
                "Etiqueta", "Dias de Empréstimo", "Revistas na Caixa");

        Caixa[] caixasRegistradas = RepositorioCaixa.PegarListaRegistrados();

        int quantidadeCaixas = 0;

        for (int i = 0; i < caixasRegistradas.Length; i++)
        {
            Caixa c = caixasRegistradas[i];

            if (c == null)
                continue;

            quantidadeCaixas++;
            RepositorioCaixa.ListaVazia = false;

            if (comId)
                Console.WriteLine(
                    "{0, -6} | {1, -20} | {2, -20} | {3, -20}",
                    c.Id, c.Etiqueta, c.DiasEmprestimo, c.Revistas.Count(r => r != null));
            else
                Console.WriteLine(
                    "{0, -20} | {1, -20} | {2, -20}",
                    c.Etiqueta, c.DiasEmprestimo, c.Revistas.Count(r => r != null));
        }

        if (quantidadeCaixas == 0)
        {
            Console.WriteLine("\nNenhuma caixa registrada!");
            RepositorioCaixa.ListaVazia = true;
        }
    }
    public void EditarCaixa()
    {
        ExibirCabecalho();

        Console.WriteLine("Editando Caixa...");
        Console.WriteLine("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioCaixa.ListaVazia)
            return;

        bool idValido;
        int idCaixaEscolhida;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.Write("Selecione o ID de uma Caixa: ");
            idValido = int.TryParse(Console.ReadLine(), out idCaixaEscolhida);

            if (!idValido)
            {
                Console.WriteLine("\nO ID selecionado é inválido!");
                Console.Write("\nPressione [Enter] para tentar novamente!");
                Console.ReadKey();
                EditarCaixa();
                return;
            }
        } while (!idValido);

        Caixa caixaEscolhida = RepositorioCaixa.SelecionarPorId(idCaixaEscolhida);

        if (caixaEscolhida == null)
        {
            Console.WriteLine("\nO ID escolhido não está registrado.");
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            EditarCaixa();
            return;
        }

        Caixa dadosEditados = ObterDadosCaixa();

        string erros = dadosEditados.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine(erros);
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            EditarCaixa();
            return;
        }

        if (RepositorioCaixa.VerificarEtiquetas(dadosEditados))
        {
            Console.WriteLine("\nJá existe uma caixa com essa etiqueta!");
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            EditarCaixa();
        }

        RepositorioCaixa.EditarCaixa(caixaEscolhida, dadosEditados);

        Console.WriteLine("\nCaixa editada com sucesso!");
    }
    public void ExcluirCaixa()
    {
        ExibirCabecalho();

        Console.WriteLine("Excluindo Caixa...");
        Console.WriteLine("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioCaixa.ListaVazia)
            return;

        bool idValido;
        int idCaixaEscolhida;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.Write("Selecione o ID de uma Caixa: ");
            idValido = int.TryParse(Console.ReadLine(), out idCaixaEscolhida);

            if (!idValido)
            {
                Console.WriteLine("\nO ID selecionado é inválido!");
                Console.Write("\nPressione [Enter] para tentar novamente!");
                Console.ReadKey();
                ExcluirCaixa();
                return;
            }
        } while (!idValido);

        Caixa caixaEscolhida = RepositorioCaixa.SelecionarPorId(idCaixaEscolhida);

        if (caixaEscolhida == null)
        {
            Console.WriteLine("\nO ID escolhido não está registrado.");
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            ExcluirCaixa();
            return;
        }

        if (RepositorioCaixa.VerificarRevistasCaixa(caixaEscolhida))
        {
            Console.WriteLine($"\nA caixa {caixaEscolhida.Etiqueta} ainda possui revistas e não pode ser excluída.");
            return;
        }

        RepositorioCaixa.ExcluirCaixa(caixaEscolhida);

        Console.WriteLine("\nCaixa excluída com sucesso!");
    }
    public Caixa ObterDadosCaixa()
    {
        Console.Write("Digite o Nome da Etiqueta da Caixa: ");
        string etiqueta = Console.ReadLine()!;

        int cor = PegarCorPaletaCores();

        ExibirCabecalho();

        Console.WriteLine("Registrando Caixa...");
        Console.WriteLine("--------------------------------------------\n");

        Console.Write("Digite o número de Dias de Empréstimo das Revistas nesta Caixa (3 para revistas comuns e 7 para revistas raras): ");
        int diasEmprestimo = Convert.ToInt32(Console.ReadLine()!);

        Caixa caixa = new Caixa(etiqueta, cor, diasEmprestimo);

        return caixa;
    }
    public int PegarCorPaletaCores()
    {
        ExibirCabecalho();

        Console.WriteLine("Paleta de Cores");
        Console.WriteLine("--------------------------------------------");

        for (int i = 0; i <= 15; i++)
        {
            Console.ForegroundColor = (ConsoleColor)i;
            Console.WriteLine($"{i} >> {(ConsoleColor)i}");
        }
        Console.ResetColor();

        bool idValido;
        int cor;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.Write("Selecione uma cor da paleta: ");
            idValido = int.TryParse(Console.ReadLine(), out cor);

            if (!idValido)
            {
                Console.WriteLine("\nEssa não é uma opção da paleta de cores.");
                Console.Write("\nPressione [Enter] para tentar novamente!");
                Console.ReadKey();
                PegarCorPaletaCores();
                break;
            }
            break;
        } while (!idValido);

        return cor;
    }
}
