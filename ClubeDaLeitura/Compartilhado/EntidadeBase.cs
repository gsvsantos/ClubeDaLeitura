
namespace ClubeDaLeitura.Compartilhado;

public abstract class EntidadeBase<T>
{
    public int Id { get; set; }

    public abstract void AtualizarRegistro(T dadosEditados);
    public abstract string Validar();
}
