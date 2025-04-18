using ClubeDaLeitura.Compartilhado;

namespace ClubeDaLeitura.ModuloReserva;

public class RepositorioReserva : RepositorioBase
{
    public override void CadastrarRegistro(EntidadeBase novoRegistro)
    {
        Reserva novaReserva = (Reserva)novoRegistro;

        novaReserva.Revista.Reservar();
        novaReserva.Amigo.ReceberReserva(novaReserva);
        base.CadastrarRegistro(novaReserva);
    }
    public bool VerificarReservaAtiva(Reserva reservaEscolhida)
    {
        if (reservaEscolhida.Status != "Ativa")
            return true;
        else
            return false;
    }
}
