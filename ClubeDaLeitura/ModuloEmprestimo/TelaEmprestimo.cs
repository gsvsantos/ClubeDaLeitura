using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloAmigo;
using ClubeDaLeitura.ModuloMulta;
using ClubeDaLeitura.ModuloRevista;

namespace ClubeDaLeitura.ModuloEmprestimo;

public class TelaEmprestimo
{
    public RepositorioAmigo RepositorioAmigo;
    public RepositorioEmprestimo RepositorioEmprestimo;
    public RepositorioRevista RepositorioRevista;
    public RepositorioMulta RepositorioMulta;

    public TelaEmprestimo(RepositorioAmigo repositorioAmigo, RepositorioEmprestimo repositorioEmprestimo, RepositorioMulta repositorioMulta, RepositorioRevista repositorioRevista)
    {
        RepositorioAmigo = repositorioAmigo;
        RepositorioEmprestimo = repositorioEmprestimo;
        RepositorioMulta = repositorioMulta;
        RepositorioRevista = repositorioRevista;
    }
    public string ApresentarMenu()
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("1 >> Registrar Empréstimo");
        ColorirEscrita.ComQuebraLinha("2 >> Visualizar Lista de Empréstimos");
        ColorirEscrita.ComQuebraLinha("3 >> Editar Empréstimo");
        ColorirEscrita.ComQuebraLinha("4 >> Excluir Empréstimo");
        ColorirEscrita.ComQuebraLinha("5 >> Registrar Devolução");
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
        ColorirEscrita.ComQuebraLinha("Gestão de Empréstimos");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------\n");
    }
    public void RegistrarEmprestimo()
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Registrando Empréstimo...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------\n");

        Emprestimo novoEmprestimo = ObterDadosEmprestimo();

        if (novoEmprestimo == null)
            return;

        string erros = novoEmprestimo.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            RegistrarEmprestimo();
            return;
        }

        RepositorioEmprestimo.RegistrarEmprestimo(novoEmprestimo);

        Notificador.ExibirMensagem("\nEmpréstimo registrado com sucesso!", ConsoleColor.Green);
    }
    public void MostrarListaRegistrados(bool exibirCabecalho, bool comId)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Visualizando Empréstimos...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------\n");

        if (comId)
        {
            string[] cabecalho = ["Id", "Amigo", "Revista", "Data de Devolução", "Situação"];
            int[] espacamentos = [6, 20, 30, 20, 20];
            ConsoleColor[] coresCabecalho = [ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.White];

            ColorirEscrita.PintarCabecalho(cabecalho, espacamentos, coresCabecalho);
        }
        else
        {
            string[] cabecalho = ["Amigo", "Revista", "Data de Devolução", "Situação"];
            int[] espacamentos = [20, 30, 20, 20];
            ConsoleColor[] coresCabecalho = [ConsoleColor.Cyan, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.White];

            ColorirEscrita.PintarCabecalho(cabecalho, espacamentos, coresCabecalho);
        }

        Emprestimo[] emprestimosRegistrados = RepositorioEmprestimo.PegarListaRegistrados();

        RepositorioEmprestimo.VerificarEmprestimosAtrasados(emprestimosRegistrados);

        int quantidadeEmprestimos = 0;

        for (int i = 0; i < emprestimosRegistrados.Length; i++)
        {
            Emprestimo e = emprestimosRegistrados[i];

            if (e == null)
                continue;

            quantidadeEmprestimos++;
            RepositorioEmprestimo.ListaVazia = false;

            if (comId)
            {
                string[] cabecalho = [e.Id.ToString(), e.Amigo.Nome, e.Revista.Titulo, e.ObterDataDevolucao().ToShortDateString(), e.Situacao];
                int[] espacamentos = [6, 20, 30, 20, 20];
                ConsoleColor[] coresCabecalho = [ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.White];

                ColorirEscrita.PintarLinha(cabecalho, espacamentos, coresCabecalho);
            }
            else
            {
                string[] cabecalho = [e.Amigo.Nome, e.Revista.Titulo, e.ObterDataDevolucao().ToShortDateString(), e.Situacao];
                int[] espacamentos = [20, 30, 20, 20];
                ConsoleColor[] coresCabecalho = [ConsoleColor.Cyan, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.White];

                ColorirEscrita.PintarLinha(cabecalho, espacamentos, coresCabecalho);
            }
        }

        if (quantidadeEmprestimos == 0)
        {
            Notificador.ExibirMensagem("\nNenhum empréstimo registrado!", ConsoleColor.Red);
            RepositorioEmprestimo.ListaVazia = true;
        }
    }
    public void EditarEmprestimo()
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Editando Empréstimo...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioEmprestimo.ListaVazia)
            return;

        bool idValido;
        int idEmprestimoEscolhido;

        do
        {
            ColorirEscrita.ComQuebraLinha("\n--------------------------------------------");
            ColorirEscrita.SemQuebraLinha("Selecione o ID de um Empréstimo: ");
            idValido = int.TryParse(Console.ReadLine(), out idEmprestimoEscolhido);

            if (!idValido)
            {
                Notificador.ExibirMensagem("\nO ID selecionado é inválido!", ConsoleColor.Red);
                ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
                Console.ReadKey();
                EditarEmprestimo();
                return;
            }
        } while (!idValido);

        Emprestimo emprestimoEscolhido = RepositorioEmprestimo.SelecionarPorId(idEmprestimoEscolhido);

        if (emprestimoEscolhido == null)
        {
            Notificador.ExibirMensagem("\nO ID escolhido não está registrado.", ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            EditarEmprestimo();
            return;
        }

        Emprestimo dadosEditados = ObterDadosEmprestimo();

        if (dadosEditados == null)
            return;

        string erros = dadosEditados.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            EditarEmprestimo();
            return;
        }

        if (RepositorioEmprestimo.VerificarDevolucao(dadosEditados))
        {
            Notificador.ExibirMensagem("\nEsse empréstimo já foi concluído.", ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            EditarEmprestimo();
            return;
        }

        RepositorioEmprestimo.EditarEmprestimo(emprestimoEscolhido, dadosEditados);

        Notificador.ExibirMensagem("\nEmpréstimo editado com sucesso!", ConsoleColor.Green);
    }
    public void ExcluirEmprestimo()
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Excluindo Empréstimo...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioEmprestimo.ListaVazia)
            return;

        bool idValido;
        int idEmprestimoEscolhido;

        do
        {
            ColorirEscrita.ComQuebraLinha("\n--------------------------------------------");
            ColorirEscrita.SemQuebraLinha("Selecione o ID de um Empréstimo: ");
            idValido = int.TryParse(Console.ReadLine(), out idEmprestimoEscolhido);

            if (!idValido)
            {
                Notificador.ExibirMensagem("\nO ID selecionado é inválido!", ConsoleColor.Red);
                ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
                Console.ReadKey();
                EditarEmprestimo();
                return;
            }
        } while (!idValido);

        Emprestimo emprestimoEscolhido = RepositorioEmprestimo.SelecionarPorId(idEmprestimoEscolhido);

        if (emprestimoEscolhido == null)
        {
            Notificador.ExibirMensagem("\nO ID escolhido não está registrado.", ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            ExcluirEmprestimo();
            return;
        }

        if (RepositorioEmprestimo.VerificarEmprestimoAtivo(emprestimoEscolhido.Amigo))
        {
            Notificador.ExibirMensagem("\nEsse empréstimo ainda está em aberto!", ConsoleColor.Red);
            return;
        }

        if (emprestimoEscolhido.Situacao == "ATRASADO")
        {
            Notificador.ExibirMensagem("\nEsse empréstimo está atrasado!", ConsoleColor.Red);
            return;
        }

        RepositorioEmprestimo.ExcluirEmprestimo(emprestimoEscolhido);

        Notificador.ExibirMensagem("\nEmpréstimo excluído com sucesso!", ConsoleColor.Green);
    }
    public void MostrarListaRevistas(bool exibirCabecalho, bool comId)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Visualizando Revistas...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------\n");

        if (comId)
        {
            string[] cabecalho = ["Id", "Título", "N° de Edição", "Ano de Publicação", "Caixa", "Status"];
            int[] espacamentos = [6, 25, 14, 18, 20, 20];
            ConsoleColor[] coresCabecalho = [ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.White];

            ColorirEscrita.PintarCabecalho(cabecalho, espacamentos, coresCabecalho);
        }
        else
        {
            string[] cabecalho = ["Título", "N° de Edição", "Ano de Publicação", "Caixa", "Status"];
            int[] espacamentos = [25, 14, 18, 20, 20];
            ConsoleColor[] coresCabecalho = [ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.White];

            ColorirEscrita.PintarCabecalho(cabecalho, espacamentos, coresCabecalho);
        }

        Revista[] revistasRegistradas = RepositorioRevista.PegarListaRegistrados();

        int quantidadeRevistas = 0;

        for (int i = 0; i < revistasRegistradas.Length; i++)
        {
            Revista r = revistasRegistradas[i];

            if (r == null)
                continue;

            quantidadeRevistas++;
            RepositorioRevista.ListaVazia = false;

            if (comId)
            {
                string[] cabecalho = [r.Id.ToString(), r.Titulo, r.NumeroEdicao.ToString(), r.AnoPublicacao, r.Caixa.Etiqueta, r.StatusEmprestimo];
                int[] espacamentos = [6, 25, 14, 18, 20, 20];
                ConsoleColor[] coresCabecalho = [ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Blue, (ConsoleColor)r.Caixa.Cor, ConsoleColor.White];

                ColorirEscrita.PintarLinha(cabecalho, espacamentos, coresCabecalho);
            }
            else
            {
                string[] cabecalho = [r.Titulo, r.NumeroEdicao.ToString(), r.AnoPublicacao, r.Caixa.Etiqueta, r.StatusEmprestimo];
                int[] espacamentos = [25, 14, 18, 20, 20];
                ConsoleColor[] coresCabecalho = [ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Blue, (ConsoleColor)r.Caixa.Cor, ConsoleColor.White];

                ColorirEscrita.PintarLinha(cabecalho, espacamentos, coresCabecalho);
            }
        }

        if (quantidadeRevistas == 0)
        {
            Notificador.ExibirMensagem("\nNenhuma revista registrada!", ConsoleColor.Red);
            RepositorioRevista.ListaVazia = true;
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
    public void RegistrarDevolucao()
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha("Devolução Empréstimo...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (RepositorioEmprestimo.ListaVazia)
            return;

        bool idValido;
        int idEmprestimoEscolhido;

        do
        {
            ColorirEscrita.ComQuebraLinha("\n--------------------------------------------");
            ColorirEscrita.SemQuebraLinha("Selecione o ID de um Empréstimo: ");
            idValido = int.TryParse(Console.ReadLine(), out idEmprestimoEscolhido);

            if (!idValido)
            {
                Notificador.ExibirMensagem("\nO ID selecionado é inválido!", ConsoleColor.Red);
                ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
                Console.ReadKey();
                RegistrarDevolucao();
                return;
            }
        } while (!idValido);

        Emprestimo emprestimoEscolhido = RepositorioEmprestimo.SelecionarPorId(idEmprestimoEscolhido);

        if (emprestimoEscolhido == null)
        {
            Notificador.ExibirMensagem("\nO ID escolhido não está registrado.", ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            RegistrarDevolucao();
            return;
        }

        if (RepositorioEmprestimo.VerificarDevolucao(emprestimoEscolhido))
        {
            Notificador.ExibirMensagem("\nA devolução escolhida não está em aberto!", ConsoleColor.Red);
            return;
        }

        if (emprestimoEscolhido.Situacao == "ATRASADO" && !emprestimoEscolhido.Amigo.Multas.Any(m => m != null && m.Emprestimo.Id == emprestimoEscolhido.Id))
        {
            Multa novaMulta = new Multa(emprestimoEscolhido);
            RepositorioMulta.RegistrarMulta(novaMulta);
            emprestimoEscolhido.Amigo.ReceberMulta(novaMulta);
        }

        emprestimoEscolhido.RegistrarDevolucao();

        Notificador.ExibirMensagem("\nDevolução feita com sucesso!", ConsoleColor.Green);
    }
    public Emprestimo ObterDadosEmprestimo()
    {
        MostrarListaAmigos(true, true);

        if (RepositorioAmigo.ListaVazia)
            return null!;

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
                return null!;
            }
        } while (!idAmigoValido);

        Amigo amigoEscolhido = RepositorioAmigo.SelecionarPorId(idAmigoEscolhido);

        if (amigoEscolhido == null)
        {
            Notificador.ExibirMensagem("\nO ID escolhido não está registrado.", ConsoleColor.Red);
            return null!;
        }

        if (amigoEscolhido.Multas.Any(m => m != null && m.Status == "Pendente"))
        {
            Notificador.ExibirMensagem("\nEsse amigo tem multas pendentes!", ConsoleColor.Red);
            return null!;
        }

        if (RepositorioEmprestimo.VerificarEmprestimoAtivo(amigoEscolhido))
        {
            Notificador.ExibirMensagem("\nEsse amigo já tem um empréstimo em aberto!", ConsoleColor.Red);
            return null!;
        }

        MostrarListaRevistas(true, true);

        if (RepositorioRevista.ListaVazia)
            return null!;

        bool idRevistaValido;
        int idRevistaEscolhida;

        do
        {
            ColorirEscrita.ComQuebraLinha("\n--------------------------------------------");
            ColorirEscrita.SemQuebraLinha("Selecione o ID de uma Revista: ");
            idRevistaValido = int.TryParse(Console.ReadLine(), out idRevistaEscolhida);

            if (!idRevistaValido)
            {
                Notificador.ExibirMensagem("\nO ID selecionado é inválido!", ConsoleColor.Red);
                return null!;
            }
        } while (!idRevistaValido);

        Revista revistaEscolhida = RepositorioRevista.SelecionarPorId(idRevistaEscolhida);

        if (!RepositorioRevista.VerificarRevistaDisponivel(revistaEscolhida))
        {
            Notificador.ExibirMensagem("\nEssa revista não está disponível!", ConsoleColor.Red);
            return null!;
        }

        if (revistaEscolhida.StatusEmprestimo == "Reservada")
        {
            Notificador.ExibirMensagem("\nEssa revista está reservada!", ConsoleColor.Red);
            return null!;
        }

        Emprestimo emprestimo = new Emprestimo(amigoEscolhido, revistaEscolhida);

        return emprestimo;
    }
}
