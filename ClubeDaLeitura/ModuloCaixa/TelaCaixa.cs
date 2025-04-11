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
        Console.Clear();
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine("1 >> Registrar Caixa");
        Console.WriteLine("2 >> Visualizar Lista de Caixas");
        Console.WriteLine("3 >> Editar Caixa");
        Console.WriteLine("4 >> Excluir Caixa");
        Console.WriteLine("S >> Voltar");

        Console.WriteLine();
        Console.Write("Opção: ");

        return Console.ReadLine()!.ToUpper();
    }
    public void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("Gestão de Caixas");
        Console.WriteLine("--------------------------------------------");
    }
    public void RegistrarCaixa()
    {
        ExibirCabecalho();

        Console.WriteLine("\nRegistrando Caixa...");
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
            Console.WriteLine("Já existe uma caixa registrada com essa etiqueta!");
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            RegistrarCaixa();
        }

        RepositorioCaixa.RegistrarCaixa(novaCaixa);
        Console.WriteLine("Caixa registrado com sucesso!");
    }
    public void MostrarListaRegistrados(bool exibirCabecalho, bool comId)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        Console.WriteLine("\nVisualizando Caixas...");
        Console.WriteLine("--------------------------------------------\n");

        if (comId)
            Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -20}",
                "Id", "Etiqueta", "Dias de Empréstimo");
        else
            Console.WriteLine(
                "{0, -20} | {1, -20}",
                "Etiqueta", "Dias de Empréstimo");

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
                    "{0, -6} | {1, -20} | {2, -20}",
                    c.Id, c.Etiqueta, c.DiasEmprestimo);
            else
                Console.WriteLine(
                    "{0, -20} | {1, -20}",
                    c.Etiqueta, c.DiasEmprestimo);
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

        Console.WriteLine("\nEditando Caixa...");
        Console.WriteLine("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioCaixa.ListaVazia)
            return;

        Console.WriteLine("\n--------------------------------------------");
        Console.Write("Selecione o ID de uma Caixa: ");
        int idCaixaEscolhida = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine();
        Caixa caixaEscolhida = RepositorioCaixa.SelecionarPorId(idCaixaEscolhida);

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
            Console.WriteLine("Já existe uma caixa com essa etiqueta!");
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            EditarCaixa();
        }

        RepositorioCaixa.EditarCaixa(caixaEscolhida, dadosEditados);

        Console.WriteLine();
        Console.WriteLine("Caixa editado com sucesso!");
    }
    public void ExcluirCaixa()
    {
        ExibirCabecalho();

        Console.WriteLine("\nExcluindo Caixa...");
        Console.WriteLine("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioCaixa.ListaVazia)
            return;

        Console.WriteLine("\n--------------------------------------------");
        Console.Write("Selecione o ID de uma Caixa: ");
        int idCaixaEscolhida = Convert.ToInt32(Console.ReadLine());

        Caixa caixaEscolhida = RepositorioCaixa.SelecionarPorId(idCaixaEscolhida);

        if (RepositorioCaixa.VerificarRevistasCaixa(caixaEscolhida))
        {
            Console.WriteLine($"Ainda tem revistas na caixa {caixaEscolhida.Etiqueta}");
            return;
        }

        RepositorioCaixa.ExcluirCaixa(caixaEscolhida);

        Console.WriteLine();
        Console.WriteLine("Caixa excluído com sucesso!");
    }
    public Caixa ObterDadosCaixa()
    {
        Console.Write("Digite o nome da Etiqueta da Caixa: ");
        string etiqueta = Console.ReadLine()!;

        Console.Write("Escolha uma cor da paleta: ");
        int cor = Convert.ToInt32(Console.ReadLine()!);

        Console.Write("Digite os dias de empréstimos das revistas nesta caixa: ");
        int diasEmprestimo = Convert.ToInt32(Console.ReadLine()!);

        Caixa caixa = new Caixa(etiqueta, cor, diasEmprestimo);

        return caixa;
    }
}
