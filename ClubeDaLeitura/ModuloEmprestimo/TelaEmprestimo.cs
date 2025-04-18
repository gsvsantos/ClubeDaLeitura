using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloAmigo;
using ClubeDaLeitura.ModuloMulta;
using ClubeDaLeitura.ModuloRevista;
using ClubeDaLeitura.Utils;

namespace ClubeDaLeitura.ModuloEmprestimo;

public class TelaEmprestimo : TelaBase
{
    public RepositorioAmigo RepositorioAmigo;
    public RepositorioEmprestimo RepositorioEmprestimo;
    public RepositorioRevista RepositorioRevista;
    public RepositorioMulta RepositorioMulta;

    public TelaEmprestimo(RepositorioAmigo repositorioAmigo, RepositorioEmprestimo repositorioEmprestimo, RepositorioMulta repositorioMulta, RepositorioRevista repositorioRevista) : base("Emprestimo", repositorioEmprestimo)
    {
        RepositorioAmigo = repositorioAmigo;
        RepositorioEmprestimo = repositorioEmprestimo;
        RepositorioMulta = repositorioMulta;
        RepositorioRevista = repositorioRevista;
    }
    public override string ApresentarMenu()
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
    public override void MostrarListaRegistrados(bool exibirCabecalho, bool comId)
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

        EntidadeBase[] registros = RepositorioEmprestimo.PegarListaRegistrados();
        Emprestimo[] emprestimosRegistrados = new Emprestimo[registros.Length];


        for (int i = 0; i < registros.Length; i++)
        {
            emprestimosRegistrados[i] = (Emprestimo)registros[i];
        }

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
    public override void EditarRegistro()
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
                EditarRegistro();
                return;
            }
        } while (!idValido);

        Emprestimo emprestimoEscolhido = (Emprestimo)RepositorioEmprestimo.SelecionarRegistroPorId(idEmprestimoEscolhido);

        if (emprestimoEscolhido == null)
        {
            Notificador.ExibirMensagem("\nO ID escolhido não está registrado.", ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            EditarRegistro();
            return;
        }

        Emprestimo dadosEditados = (Emprestimo)ObterDados();

        if (dadosEditados == null)
            return;

        string erros = dadosEditados.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            EditarRegistro();
            return;
        }

        if (RepositorioEmprestimo.VerificarDevolucao(dadosEditados))
        {
            Notificador.ExibirMensagem("\nEsse empréstimo já foi concluído.", ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            EditarRegistro();
            return;
        }

        RepositorioEmprestimo.EditarRegistro(emprestimoEscolhido, dadosEditados);

        Notificador.ExibirMensagem("\nEmpréstimo editado com sucesso!", ConsoleColor.Green);
    }
    public override void ExcluirRegistro()
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
                ExcluirRegistro();
                return;
            }
        } while (!idValido);

        Emprestimo emprestimoEscolhido = (Emprestimo)RepositorioEmprestimo.SelecionarRegistroPorId(idEmprestimoEscolhido);

        if (emprestimoEscolhido == null)
        {
            Notificador.ExibirMensagem("\nO ID escolhido não está registrado.", ConsoleColor.Red);
            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para novamente.", ConsoleColor.Yellow);
            Console.ReadKey();
            ExcluirRegistro();
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

        RepositorioEmprestimo.ExcluirRegistro(emprestimoEscolhido);

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
            int[] espacamentos = [3, 24, 14, 18, 20, 18];
            ConsoleColor[] coresCabecalho = [ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.White];

            ColorirEscrita.PintarCabecalho(cabecalho, espacamentos, coresCabecalho);
        }
        else
        {
            string[] cabecalho = ["Título", "N° de Edição", "Ano de Publicação", "Caixa", "Status"];
            int[] espacamentos = [24, 14, 18, 20, 18];
            ConsoleColor[] coresCabecalho = [ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.White];

            ColorirEscrita.PintarCabecalho(cabecalho, espacamentos, coresCabecalho);
        }

        EntidadeBase[] registros = RepositorioRevista.PegarListaRegistrados();
        Revista[] revistasRegistradas = new Revista[registros.Length];

        for (int i = 0; i < registros.Length; i++)
        {
            revistasRegistradas[i] = (Revista)registros[i];
        }

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
                int[] espacamentos = [3, 24, 14, 18, 20, 18];
                ConsoleColor[] coresCabecalho = [ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Blue, (ConsoleColor)r.Caixa.Cor, ConsoleColor.White];

                ColorirEscrita.PintarLinha(cabecalho, espacamentos, coresCabecalho);
            }
            else
            {
                string[] cabecalho = [r.Titulo, r.NumeroEdicao.ToString(), r.AnoPublicacao, r.Caixa.Etiqueta, r.StatusEmprestimo];
                int[] espacamentos = [24, 14, 18, 20, 18];
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

        EntidadeBase[] registros = RepositorioAmigo.PegarListaRegistrados();
        Amigo[] amigosRegistrados = new Amigo[registros.Length];

        for (int i = 0; i < registros.Length; i++)
        {
            amigosRegistrados[i] = (Amigo)registros[i];
        }

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

        Emprestimo emprestimoEscolhido = (Emprestimo)RepositorioEmprestimo.SelecionarRegistroPorId(idEmprestimoEscolhido);

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
            RepositorioMulta.CadastrarRegistro(novaMulta);
            emprestimoEscolhido.Amigo.ReceberMulta(novaMulta);
        }

        emprestimoEscolhido.RegistrarDevolucao();

        Notificador.ExibirMensagem("\nDevolução feita com sucesso!", ConsoleColor.Green);
    }
    public override EntidadeBase ObterDados()
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
                Notificador.ExibirMensagem("\nO ID selecionado é inválido!", ConsoleColor.Red);
        } while (!idAmigoValido);

        Amigo amigoEscolhido = (Amigo)RepositorioAmigo.SelecionarRegistroPorId(idAmigoEscolhido);

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
                Notificador.ExibirMensagem("\nO ID selecionado é inválido!", ConsoleColor.Red);
        } while (!idRevistaValido);

        Revista revistaEscolhida = (Revista)RepositorioRevista.SelecionarRegistroPorId(idRevistaEscolhida);

        Emprestimo emprestimo = new Emprestimo(amigoEscolhido, revistaEscolhida);

        return emprestimo;
    }
}
