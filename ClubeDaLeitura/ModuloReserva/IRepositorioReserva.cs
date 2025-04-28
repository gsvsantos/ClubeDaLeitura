using ClubeDaLeitura.Compartilhado;

namespace ClubeDaLeitura.ModuloReserva;

public interface IRepositorioReserva : IRepositorio<Reserva>
{
    public bool VerificarReservaAtiva(Reserva reservaEscolhida);
}
