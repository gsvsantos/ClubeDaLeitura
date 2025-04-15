namespace ClubeDaLeitura.ModuloReserva;

public class RepositorioReserva
{
    public Reserva[] Reservas = new Reserva[100];
    public int IndiceListaReservas = 0;
    public bool ListaVazia = false;

    public void RegistrarReserva(Reserva novaReserva)
    {
        novaReserva.GerarId();
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
}
