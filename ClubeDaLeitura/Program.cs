﻿using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloAmigo;
using ClubeDaLeitura.ModuloCaixa;
using ClubeDaLeitura.ModuloEmprestimo;
using ClubeDaLeitura.ModuloMulta;
using ClubeDaLeitura.ModuloReserva;
using ClubeDaLeitura.ModuloRevista;

namespace ClubeDaLeitura;

public class Program
{
    static void Main(string[] args)
    {
        RepositorioAmigo repositorioAmigo = new RepositorioAmigo();
        RepositorioCaixa repositorioCaixa = new RepositorioCaixa();
        RepositorioEmprestimo repositorioEmprestimo = new RepositorioEmprestimo();
        RepositorioMulta repositorioMulta = new RepositorioMulta();
        RepositorioReserva repositorioReserva = new RepositorioReserva();
        RepositorioRevista repositorioRevista = new RepositorioRevista();

        TelaAmigo telaAmigo = new TelaAmigo(repositorioAmigo, repositorioEmprestimo);
        TelaCaixa telaCaixa = new TelaCaixa(repositorioCaixa);
        TelaEmprestimo telaEmprestimo = new TelaEmprestimo(repositorioAmigo, repositorioEmprestimo, repositorioMulta, repositorioRevista);
        TelaMulta telaMulta = new TelaMulta(repositorioAmigo, repositorioEmprestimo, repositorioMulta);
        TelaReserva telaReserva = new TelaReserva(repositorioAmigo, repositorioEmprestimo, repositorioReserva, repositorioRevista);
        TelaRevista telaRevista = new TelaRevista(repositorioCaixa, repositorioRevista);

        do
        {
            string[] opcoesValidas = ["1", "2", "3", "4", "5", "6", "S"];

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
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                        case "2":
                            telaAmigo.MostrarListaRegistrados(true, false);
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                        case "3":
                            telaAmigo.MostrarListaEmprestimos(true, false);
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                        case "4":
                            telaAmigo.EditarAmigo();
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                        case "5":
                            telaAmigo.ExcluirAmigo();
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                        case "S":
                            menuAmigo = false;
                            continue;
                        default:
                            Notificador.ExibirMensagem("\nOpção inválida!", ConsoleColor.Red);
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
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
                    string opcaoMenuCaixa = telaCaixa.ApresentarMenu();
                    switch (opcaoMenuCaixa)
                    {
                        case "1":
                            telaCaixa.RegistrarCaixa();
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                        case "2":
                            telaCaixa.MostrarListaRegistrados(true, false);
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                        case "3":
                            telaCaixa.EditarCaixa();
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                        case "4":
                            telaCaixa.ExcluirCaixa();
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                        case "S":
                            menuCaixa = false;
                            continue;
                        default:
                            Notificador.ExibirMensagem("\nOpção inválida!", ConsoleColor.Red);
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
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
                    string opcaoMenuRevista = telaRevista.ApresentarMenu();
                    switch (opcaoMenuRevista)
                    {
                        case "1":
                            telaRevista.RegistrarRevista();
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                        case "2":
                            telaRevista.MostrarListaRegistrados(true, false);
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                        case "3":
                            telaRevista.EditarRevista();
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                        case "4":
                            telaRevista.ExcluirRevista();
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                        case "S":
                            menuRevista = false;
                            continue;
                        default:
                            Notificador.ExibirMensagem("\nOpção inválida!", ConsoleColor.Red);
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
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
                    string opcaoMenuEmprestimo = telaEmprestimo.ApresentarMenu();
                    switch (opcaoMenuEmprestimo)
                    {
                        case "1":
                            telaEmprestimo.RegistrarEmprestimo();
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                        case "2":
                            telaEmprestimo.MostrarListaRegistrados(true, false);
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                        case "3":
                            telaEmprestimo.EditarEmprestimo();
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                        case "4":
                            telaEmprestimo.ExcluirEmprestimo();
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                        case "5":
                            telaEmprestimo.RegistrarDevolucao();
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                        case "S":
                            menuEmprestimo = false;
                            continue;
                        default:
                            Notificador.ExibirMensagem("\nOpção inválida!", ConsoleColor.Red);
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                    }
                }
            }
            if (opcao == "5")
            {
                bool menuMulta = true;
                while (menuMulta)
                {
                    string opcaoMenuMulta = telaMulta.ApresentarMenu();
                    switch (opcaoMenuMulta)
                    {
                        case "1":
                            telaMulta.MostrarMultasPendentes(true, false);
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                        case "2":
                            telaMulta.PagarMulta();
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                        case "3":
                            telaMulta.MostrarMultasAmigo(true, false);
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                        case "S":
                            menuMulta = false;
                            continue;
                        default:
                            Notificador.ExibirMensagem("\nOpção inválida!", ConsoleColor.Red);
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                    }
                }
            }
            if (opcao == "6")
            {
                bool MenuReserva = true;
                while (MenuReserva)
                {
                    string opcaoMenuReserva = telaReserva.ApresentarMenu();
                    switch (opcaoMenuReserva)
                    {
                        case "1":
                            telaReserva.RegistrarReserva();
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                        case "2":
                            telaReserva.CancelarReserva();
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                        case "3":
                            telaReserva.MostrarListaRegistrados(true, false);
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                        case "4":
                            telaReserva.EmprestarRevistaReservada();
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                            Console.ReadKey();
                            break;
                        case "S":
                            MenuReserva = false;
                            continue;
                        default:
                            Notificador.ExibirMensagem("\nOpção inválida!", ConsoleColor.Red);
                            ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
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
                Notificador.ExibirMensagem("\nOpção inválida!", ConsoleColor.Red);
                ColorirEscrita.SemQuebraLinha("\nPressione [Enter] para continuar.", ConsoleColor.Yellow);
                Console.ReadKey();
            }
        } while (true);
    }
}

