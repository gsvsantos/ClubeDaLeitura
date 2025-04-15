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
    public void Cancelar()
    {
        Revista.StatusEmprestimo = "Disponível";
    }
    public void Concluir()
    {
        Status = "Concluída";
        Revista.StatusEmprestimo = "Disponível";
    }
}
