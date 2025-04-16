namespace ClubeDaLeitura.ModuloReserva;

public class RepositorioReserva
{
    public Reserva[] Reservas = new Reserva[100];
    public int IndiceListaReservas = 0;
    public bool ListaVazia = false;

    public void RegistrarReserva(Reserva novaReserva)
    {
        novaReserva.GerarId();
        novaReserva.Revista.Reservar();
        novaReserva.Amigo.ReceberReserva(novaReserva);
        Reservas[IndiceListaReservas++] = novaReserva;
    }
    public Reserva[] PegarListaAtivas()
    {
        Reserva[] reservasAtivas = new Reserva[100];

        for (int i = 0; i < Reservas.Length; i++)
        {
            if (Reservas[i] == null)
                continue;

            if (Reservas[i].Status == "Ativa")
                reservasAtivas[i] = Reservas[i];
        }

        return reservasAtivas;
    }
    public void ExcluirReserva(Reserva reservaEscolhida)
    {
        for (int i = 0; i < Reservas.Length; i++)
        {
            if (Reservas[i] == null)
                continue;
            else if (Reservas[i].Id == reservaEscolhida.Id)
            {
                reservaEscolhida.Cancelar();
                Reservas[i] = null!;
                break;
            }
        }
    }
    public Reserva SelecionarPorId(int idReservaEscolhida)
    {
        foreach (Reserva r in Reservas)
        {
            if (r == null)
                continue;

            if (r.Id == idReservaEscolhida)
                return r;
        }

        return null!;
    }
    public bool VerificarReservaAtiva(Reserva reservaEscolhida)
    {
        if (reservaEscolhida.Status != "Ativa")
            return true;
        else
            return false;
    }
}
