using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloEmprestimo;

namespace ClubeDaLeitura.ModuloMulta;

public class Multa : Entidade
{
    public Emprestimo Emprestimo;
    public int DiasAtraso;
    public double ValorMulta;
    public string Status;
    private static int id = 0;

    public Multa(Emprestimo emprestimo)
    {
        Emprestimo = emprestimo;
        DiasAtraso = CalcularDiasAtraso();
        ValorMulta = CalcularValorMulta();
        Status = "Pendente";
    }
    public void GerarId()
    {
        Id = ++id;
    }
    public double CalcularValorMulta()
    {
        return 2.0 * DiasAtraso;
    }
    public int CalcularDiasAtraso()
    {
        return DateTime.Now.Day - Emprestimo.ObterDataDevolucao().Day;
    }
    public void PagarMulta()
    {
        Status = "Quitada";
    }
}
