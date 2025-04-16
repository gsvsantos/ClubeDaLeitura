using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloAmigo;
using ClubeDaLeitura.ModuloRevista;

namespace ClubeDaLeitura.ModuloReserva;

public class Reserva : Entidade
{
    public Amigo Amigo;
    public Revista Revista;
    public DateTime DataReserva;
    public string Status;
    private static int id = 0;

    public Reserva(Amigo amigo, Revista revista)
    {
        Amigo = amigo;
        Revista = revista;
        DataReserva = DateTime.Now;
        Status = "Ativa";
    }
    public void GerarId()
    {
        Id = ++id;
    }
    public string Validar()
    {
        string erros = "";

        if (Amigo.Multas.Any(m => m != null && m.Status == "Pendente"))
            erros += "\nEsse amigo tem multas pendentes!";

        if (Revista.StatusEmprestimo != "Disponível")
            erros += "\nEssa revista não está disponível.";

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
}
