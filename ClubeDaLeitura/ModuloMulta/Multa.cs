using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloEmprestimo;

namespace ClubeDaLeitura.ModuloMulta;

public class Multa : EntidadeBase<Multa>
{
    public Emprestimo Emprestimo;
    public int DiasAtraso;
    public double ValorMulta;
    public string Status;

    public Multa(Emprestimo emprestimo)
    {
        Emprestimo = emprestimo;
        DiasAtraso = CalcularDiasAtraso();
        ValorMulta = CalcularValorMulta();
        Status = "Pendente";
    }
    public override string Validar()
    {
        throw new NotImplementedException();
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
    public override void AtualizarRegistro(Multa dadosEditados) { }
}
