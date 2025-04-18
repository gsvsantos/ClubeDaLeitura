﻿using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloAmigo;
using ClubeDaLeitura.ModuloRevista;

namespace ClubeDaLeitura.ModuloEmprestimo;

public class Emprestimo : EntidadeBase
{
    public Amigo Amigo;
    public Revista Revista;
    public DateTime Data;
    public string Situacao;

    public Emprestimo(Amigo amigo, Revista revista, DateTime? data = null)
    {
        Amigo = amigo;
        Revista = revista;   // TRAZER VERSÃO CERTA APÓS TESTES!@!!!!!@!!!!
        Data = data ?? DateTime.Now;
        Situacao = "Aberto";
    }
    public override string Validar()
    {
        string erros = "";

        if (Amigo == null)
            erros += "\nO amigo selecionado náo está registrado.\n";
        else
        {
            if (Amigo.Multas.Any(m => m != null && m.Status == "Pendente"))
                erros += "O amigo selecionado tem multas pendentes.\n";

            if (Amigo.Emprestimos.Any(e => e != null && (e.Situacao == "Aberto" || e.Situacao == "ATRASADO")))
                erros += "O amigo selecionado tem um empréstimo em aberto.\n";

        }

        if (Revista == null)
            erros += "\nA revista selecionada não está registrada.\n";
        else
        {
            if (Revista.StatusEmprestimo != "Disponível")
                erros += "A revista selecionada não está disponível.\n";

            if (Revista.StatusEmprestimo == "Reservada")
                erros += "A revista selecionada está reservada.\n";
        }

        if (string.IsNullOrEmpty(Situacao))
            erros += "\nCampo 'Situacao' é obrigatório.\n";
        else
        {
            if (Situacao != "Aberto" && Situacao != "Concluído")
                erros += "Campo 'Situacao' precisa ser 'Aberto' ou 'Concluído'!\n";
        }

        return erros;
    }
    public void RegistrarDevolucao()
    {
        Situacao = "Concluído";
        Revista.Devolver();
    }
    public DateTime ObterDataDevolucao()
    {
        return Data.AddDays(Revista.Caixa.DiasEmprestimo);
    }

    public override void AtualizarRegistro(EntidadeBase dadosEditados)
    {
        Emprestimo empretimoEditado = (Emprestimo)dadosEditados;

        Amigo = empretimoEditado.Amigo;
        Revista = empretimoEditado.Revista;
        Situacao = empretimoEditado.Situacao;
    }
}
