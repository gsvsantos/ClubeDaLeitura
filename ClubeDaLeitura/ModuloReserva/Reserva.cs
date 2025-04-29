using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloAmigo;
using ClubeDaLeitura.ModuloRevista;

namespace ClubeDaLeitura.ModuloReserva;

public class Reserva : EntidadeBase<Reserva>
{
    public Amigo Amigo { get; set; }
    public Revista Revista { get; set; }
    public DateTime DataReserva { get; set; }
    public string Status { get; set; }

    public Reserva() { }
    public Reserva(Amigo amigo, Revista revista)
    {
        Amigo = amigo;
        Revista = revista;
        DataReserva = DateTime.Now;
        Status = "Ativa";
    }
    public override string Validar()
    {
        string erros = "";

        if (Amigo == null)
            erros += "\nO amigo selecionado não está registrado.\n";
        else
        {
            if (Amigo.VerificarMultas())
                erros += "O amigo selecionado tem multas pendentes.\n";

            if (Amigo.VerificarEmprestimosAbertos())
                erros += "O amigo selecionado tem um empréstimo em aberto.\n";

            if (Amigo.Reserva != null && Amigo.Reserva.Status == "Ativa")
                erros += "O amigo selecionado já tem uma reserva ativa.\n";
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

        return erros;
    }
    public void Concluir()
    {
        Status = "Concluída";
    }
    public void Cancelar()
    {
        Revista.StatusEmprestimo = "Disponível";
    }
    public override void AtualizarRegistro(Reserva dadosEditados) { }
}
