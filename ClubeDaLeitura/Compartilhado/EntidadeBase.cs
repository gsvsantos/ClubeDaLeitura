
namespace ClubeDaLeitura.Compartilhado;

public abstract class EntidadeBase<TEntidade>
{
    public int Id { get; set; }

    public abstract void AtualizarRegistro(TEntidade dadosEditados);
    public abstract string Validar();
}
