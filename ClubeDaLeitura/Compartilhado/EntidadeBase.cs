
namespace ClubeDaLeitura.Compartilhado;

public abstract class EntidadeBase
{
    public int Id { get; set; }

    public abstract void AtualizarRegistro(EntidadeBase dadosEditados);
    public abstract string Validar();
}
