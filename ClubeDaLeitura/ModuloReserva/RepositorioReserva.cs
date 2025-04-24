using ClubeDaLeitura.Compartilhado;

namespace ClubeDaLeitura.ModuloReserva;

public class RepositorioReserva : RepositorioBase<Reserva>
{
    public override void CadastrarRegistro(Reserva novoRegistro)
    {
        novoRegistro.Revista.Reservar();
        novoRegistro.Amigo.ReceberReserva(novoRegistro);
        base.CadastrarRegistro(novoRegistro);
    }
    public bool VerificarReservaAtiva(Reserva reservaEscolhida)
    {
        if (reservaEscolhida.Status != "Ativa")
            return true;
        else
            return false;
    }
}
