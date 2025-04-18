using ClubeDaLeitura.Compartilhado;

namespace ClubeDaLeitura.ModuloMulta;

public class RepositorioMulta : RepositorioBase
{
    public Multa[] PegarListaMultasPendentes()
    {
        Multa[] multasPendentes = new Multa[Registros.Length];

        for (int i = 0; i < Registros.Length; i++)
        {
            if (Registros[i] == null)
                continue;

            Multa multa = (Multa)Registros[i];

            if (multa.Status == "Pendente")
                multasPendentes[i] = multa;
        }

        return multasPendentes;
    }
    public bool VerificarMultaQuitada(Multa multaEscolhida)
    {
        if (multaEscolhida == null)
            return false;

        if (multaEscolhida.Status == "Quitada")
            return true;
        else
            return false;
    }
}
