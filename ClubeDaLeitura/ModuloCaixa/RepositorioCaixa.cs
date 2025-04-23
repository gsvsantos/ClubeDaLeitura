using ClubeDaLeitura.Compartilhado;

namespace ClubeDaLeitura.ModuloCaixa;
public class RepositorioCaixa : RepositorioBase
{
    public bool VerificarEtiquetasNovoRegistro(Caixa novaCaixa)
    {
        for (int i = 0; i < Registros.Count; i++)
        {
            if (Registros[i] == null)
                continue;

            Caixa caixa = (Caixa)Registros[i]!;

            if (novaCaixa.Etiqueta == caixa.Etiqueta)
                return true;
        }

        return false;
    }
    public bool VerificarEtiquetasEditarRegistro(Caixa caixaEscolhida, Caixa dadosEditados)
    {
        for (int i = 0; i < Registros.Count; i++)
        {
            if (Registros[i] == null)
                continue;

            Caixa caixa = (Caixa)Registros[i]!;

            if (dadosEditados.Etiqueta == caixa.Etiqueta && caixaEscolhida.Id != caixa.Id)
                return true;
        }

        return false;
    }
}
