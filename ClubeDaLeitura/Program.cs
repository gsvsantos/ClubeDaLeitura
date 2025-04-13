using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloAmigo;
using ClubeDaLeitura.ModuloCaixa;
using ClubeDaLeitura.ModuloEmprestimo;
using ClubeDaLeitura.ModuloRevista;

namespace ClubeDaLeitura;

public class Program
{
    static void Main(string[] args)
    {
        RepositorioAmigo repositorioAmigo = new RepositorioAmigo();
        RepositorioCaixa repositorioCaixa = new RepositorioCaixa();
        RepositorioEmprestimo repositorioEmprestimo = new RepositorioEmprestimo();
        RepositorioRevista repositorioRevista = new RepositorioRevista();

        TelaAmigo telaAmigo = new TelaAmigo(repositorioAmigo);
        TelaCaixa telaCaixa = new TelaCaixa(repositorioCaixa);
        TelaEmprestimo telaEmprestimo = new TelaEmprestimo(repositorioEmprestimo, repositorioAmigo, repositorioRevista);
        TelaRevista telaRevista = new TelaRevista(repositorioRevista, repositorioCaixa);

        do
        {
            string[] opcoesValidas = ["1", "2", "3", "4", "S"];

            string opcao = MenuPrincipal.ApresentarMenuPrincipal();

            if (opcao == "1")
            {
                bool menuAmigo = true;
                while (menuAmigo)
                {
                    string opcaoMenuAmigo = telaAmigo.ApresentarMenu();
                    switch (opcaoMenuAmigo)
                    {
                        case "1":
                            telaAmigo.RegistrarAmigo();
                            Console.WriteLine();
                            Console.Write("Pressione [Enter] para continuar.");
                            Console.ReadKey();
                            break;
                        case "2":
                            telaAmigo.MostrarListaRegistrados(true, false);
                            Console.WriteLine();
                            Console.Write("Pressione [Enter] para continuar.");
                            Console.ReadKey();
                            break;
                        case "3":
                            telaAmigo.MostrarListaEmprestimos(true, false);
                            Console.WriteLine();
                            Console.Write("Pressione [Enter] para continuar.");
                            Console.ReadKey();
                            break;
                        case "4":
                            telaAmigo.EditarAmigo();
                            Console.WriteLine();
                            Console.Write("Pressione [Enter] para continuar.");
                            Console.ReadKey();
                            break;
                        case "5":
                            telaAmigo.ExcluirAmigo();
                            Console.WriteLine();
                            Console.Write("Pressione [Enter] para continuar.");
                            Console.ReadKey();
                            break;
                        case "S":
                            menuAmigo = false;
                            continue;
                        default:
                            Console.WriteLine("Opção inválida!\nPressione [Enter] para continuar.");
                            Console.ReadKey();
                            break;
                    }
                }
            }
            if (opcao == "2")
            {
                bool menuCaixa = true;
                while (menuCaixa)
                {
                    string opcaoMenuAmigo = telaCaixa.ApresentarMenu();
                    switch (opcaoMenuAmigo)
                    {
                        case "1":
                            telaCaixa.RegistrarCaixa();
                            Console.WriteLine();
                            Console.Write("Pressione [Enter] para continuar.");
                            Console.ReadKey();
                            break;
                        case "2":
                            telaCaixa.MostrarListaRegistrados(true, false);
                            Console.WriteLine();
                            Console.Write("Pressione [Enter] para continuar.");
                            Console.ReadKey();
                            break;
                        case "3":
                            telaCaixa.EditarCaixa();
                            Console.WriteLine();
                            Console.Write("Pressione [Enter] para continuar.");
                            Console.ReadKey();
                            break;
                        case "4":
                            telaCaixa.ExcluirCaixa();
                            Console.WriteLine();
                            Console.Write("Pressione [Enter] para continuar.");
                            Console.ReadKey();
                            break;
                        case "S":
                            menuCaixa = false;
                            continue;
                        default:
                            Console.WriteLine("Opção inválida!\nPressione [Enter] para continuar.");
                            Console.ReadKey();
                            break;
                    }
                }
            }
            if (opcao == "3")
            {
                bool menuRevista = true;
                while (menuRevista)
                {
                    string opcaoMenuAmigo = telaRevista.ApresentarMenu();
                    switch (opcaoMenuAmigo)
                    {
                        case "1":
                            telaRevista.RegistrarRevista();
                            Console.WriteLine();
                            Console.Write("Pressione [Enter] para continuar.");
                            Console.ReadKey();
                            break;
                        case "2":
                            telaRevista.MostrarListaRegistrados(true, false);
                            Console.WriteLine();
                            Console.Write("Pressione [Enter] para continuar.");
                            Console.ReadKey();
                            break;
                        case "3":
                            telaRevista.EditarRevista();
                            Console.WriteLine();
                            Console.Write("Pressione [Enter] para continuar.");
                            Console.ReadKey();
                            break;
                        case "4":
                            telaRevista.ExcluirRevista();
                            Console.WriteLine();
                            Console.Write("Pressione [Enter] para continuar.");
                            Console.ReadKey();
                            break;
                        case "S":
                            menuRevista = false;
                            continue;
                        default:
                            Console.WriteLine("Opção inválida!\nPressione [Enter] para continuar.");
                            Console.ReadKey();
                            break;
                    }
                }
            }
            if (opcao == "4")
            {
                bool menuEmprestimo = true;
                while (menuEmprestimo)
                {
                    string opcaoMenuAmigo = telaEmprestimo.ApresentarMenu();
                    switch (opcaoMenuAmigo)
                    {
                        case "1":
                            telaEmprestimo.RegistrarEmprestimo();
                            Console.WriteLine();
                            Console.Write("Pressione [Enter] para continuar.");
                            Console.ReadKey();
                            break;
                        case "2":
                            telaEmprestimo.MostrarListaRegistrados(true, false);
                            Console.WriteLine();
                            Console.Write("Pressione [Enter] para continuar.");
                            Console.ReadKey();
                            break;
                        case "3":
                            telaEmprestimo.EditarEmprestimo();
                            Console.WriteLine();
                            Console.Write("Pressione [Enter] para continuar.");
                            Console.ReadKey();
                            break;
                        case "4":
                            telaEmprestimo.ExcluirEmprestimo();
                            Console.WriteLine();
                            Console.Write("Pressione [Enter] para continuar.");
                            Console.ReadKey();
                            break;
                        case "5":
                            telaEmprestimo.RegistrarDevolucao();
                            Console.WriteLine();
                            Console.Write("Pressione [Enter] para continuar.");
                            Console.ReadKey();
                            break;
                        case "S":
                            menuEmprestimo = false;
                            continue;
                        default:
                            Console.WriteLine("Opção inválida!\nPressione [Enter] para continuar.");
                            Console.ReadKey();
                            break;
                    }
                }
            }
            if (opcao == "S")
            {
                Console.Clear();
                Console.WriteLine("Adeus (T_T)/\n\n");
                return;
            }
            if (!opcoesValidas.Contains(opcao))
            {
                Console.Write("\nOpção inválida!\nPressione [Enter] para continuar.");
                Console.ReadKey();
            }
        } while (true);
    }
}
