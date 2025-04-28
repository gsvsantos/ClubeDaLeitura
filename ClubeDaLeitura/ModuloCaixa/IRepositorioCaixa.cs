using ClubeDaLeitura.Compartilhado;

namespace ClubeDaLeitura.ModuloCaixa;

public interface IRepositorioCaixa : IRepositorio<Caixa>
{
    public bool VerificarEtiquetasNovoRegistro(Caixa novaCaixa);
    public bool VerificarEtiquetasEditarRegistro(Caixa caixaEscolhida, Caixa dadosEditados);
}
