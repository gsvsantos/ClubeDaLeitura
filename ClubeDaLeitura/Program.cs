using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloAmigo;
using ClubeDaLeitura.ModuloEmprestimo;
using ClubeDaLeitura.ModuloMulta;
using ClubeDaLeitura.ModuloReserva;
using ClubeDaLeitura.Utils;

namespace ClubeDaLeitura;

public class Program
{
    static void Main(string[] args)
    {
        MenuPrincipal menuPrincipal = new MenuPrincipal();

        do
        {
            string[] opcoesValidas = ["1", "2", "3", "4", "5", "6", "S"];

            menuPrincipal.ApresentarMenuPrincipal();

            TelaBase telaSelecionada = menuPrincipal.ObterTela();

            if (menuPrincipal.OpcaoPrincipal != null && menuPrincipal.OpcaoPrincipal.ToUpper() == "S")
            {
                Console.Clear();
                Notificador.ExibirMensagem("Adeus (T_T)/\n\n", ConsoleColor.Blue);
                return;

            }
            if (telaSelecionada == null)
            {
                Notificador.ExibirMensagem("\nOpção inválida!", ConsoleColor.Red);
                ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                Console.ReadKey();
                continue;
            }

            bool menuSelecionado = true;
            while (menuSelecionado)
            {
                string opcaoEscolhida = telaSelecionada.ApresentarMenu();

                if (!opcoesValidas.Contains(opcaoEscolhida))
                {
                    Notificador.ExibirMensagem("\nOpção inválida!", ConsoleColor.Red);
                    ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                    Console.ReadKey();
                    continue;
                }

                if (telaSelecionada is TelaAmigo)
                {
                    TelaAmigo telaAmigo = (TelaAmigo)telaSelecionada;

                    if (opcaoEscolhida == "5")
                    {
                        telaAmigo.MostrarListaEmprestimos(true, false);
                        ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                        Console.ReadKey();
                        continue;
                    }
                }
                else if (telaSelecionada is TelaEmprestimo)
                {
                    TelaEmprestimo telaEmprestimo = (TelaEmprestimo)telaSelecionada;

                    if (opcaoEscolhida == "5")
                    {
                        telaEmprestimo.RegistrarDevolucao();
                        ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                        Console.ReadKey();
                        continue;
                    }
                }
                else if (telaSelecionada is TelaMulta)
                {
                    TelaMulta telaMulta = (TelaMulta)telaSelecionada;

                    switch (opcaoEscolhida)
                    {
                        case "1":
                            telaMulta.MostrarListaRegistrados(true, false);
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            continue;
                        case "2":
                            telaMulta.PagarMulta();
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            continue;
                        case "3":
                            telaMulta.MostrarMultasAmigo(true, false);
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            continue;
                    }
                }
                else if (telaSelecionada is TelaReserva)
                {
                    TelaReserva telaReserva = (TelaReserva)telaSelecionada;

                    switch (opcaoEscolhida)
                    {
                        case "1":
                            telaReserva.CadastrarRegistro();
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            continue;
                        case "2":
                            telaReserva.CancelarReserva();
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            continue;
                        case "3":
                            telaReserva.MostrarListaRegistrados(true, false);
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            continue;
                        case "4":
                            telaReserva.EmprestarRevistaReservada();
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            continue;
                    }
                }

                switch (opcaoEscolhida)
                {
                    case "1":
                        telaSelecionada.CadastrarRegistro();
                        ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                        Console.ReadKey();
                        break;
                    case "2":
                        telaSelecionada.MostrarListaRegistrados(true, false);
                        ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                        Console.ReadKey();
                        break;
                    case "3":
                        telaSelecionada.EditarRegistro();
                        ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                        Console.ReadKey();
                        break;
                    case "4":
                        telaSelecionada.ExcluirRegistro();
                        ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                        Console.ReadKey();
                        break;
                    case "S":
                        menuSelecionado = false;
                        break;
                    default:
                        Notificador.ExibirMensagem("\nOpção inválida!", ConsoleColor.Red);
                        ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                        Console.ReadKey();
                        break;
                }
            }
        } while (true);
    }
}
