using System.Text.Json;
using System.Text.Json.Serialization;
using ClubeDaLeitura.ModuloAmigo;
using ClubeDaLeitura.ModuloCaixa;
using ClubeDaLeitura.ModuloEmprestimo;
using ClubeDaLeitura.ModuloMulta;
using ClubeDaLeitura.ModuloReserva;
using ClubeDaLeitura.ModuloRevista;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ClubeDaLeitura.Compartilhado;

public class ContextoDados
{
    public List<Amigo> RegistroAmigos { get; set; }
    public List<Caixa> RegistroCaixas { get; set; }
    public List<Emprestimo> RegistroEmprestimos { get; set; }
    public List<Multa> RegistroMultas { get; set; }
    public List<Reserva> RegistroReservas { get; set; }
    public List<Revista> RegistroRevistas { get; set; }

    private string pastaRegistros = @"C:\RegistrosClubeDaLeitura";
    private string arquivoRegistros = "Registros.json";

    public ContextoDados()
    {
        RegistroAmigos = new List<Amigo>();
        RegistroCaixas = new List<Caixa>();
        RegistroEmprestimos = new List<Emprestimo>();
        RegistroMultas = new List<Multa>();
        RegistroReservas = new List<Reserva>();
        RegistroRevistas = new List<Revista>();
    }
    public ContextoDados(bool carregarRegistros) : this()
    {
        if (carregarRegistros)
            CarregarContexto();
    }
    public void SalvarContexto()
    {
        string caminhoCompletoArquivo = Path.Combine(pastaRegistros, arquivoRegistros);
        string caminhoPDF = Path.Combine(pastaRegistros, "registros.pdf");

        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;
        options.PropertyNameCaseInsensitive = true;
        options.ReferenceHandler = ReferenceHandler.Preserve;

        string registroJson = JsonSerializer.Serialize(this, options);

        if (!Directory.Exists(pastaRegistros))
            Directory.CreateDirectory(pastaRegistros);

        File.WriteAllText(caminhoCompletoArquivo, registroJson);

        using (MemoryStream ms = new MemoryStream())
        {
            using (Document doc = new Document())
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, ms);

                doc.Open();
                doc.Add(new Paragraph(registroJson));
                doc.Close();
            }

            byte[] pdfBytes = ms.ToArray();
            File.WriteAllBytes(caminhoPDF, pdfBytes);
        }
    }
    public void CarregarContexto()
    {
        string caminhoCompletoArquivo = Path.Combine(pastaRegistros, arquivoRegistros);

        if (!File.Exists(caminhoCompletoArquivo))
            return;

        JsonSerializerOptions options = new JsonSerializerOptions();
        options.ReferenceHandler = ReferenceHandler.Preserve;

        string registroJson = File.ReadAllText(caminhoCompletoArquivo);

        if (string.IsNullOrWhiteSpace(registroJson))
            return;

        ContextoDados contextoRegistrado = JsonSerializer.Deserialize<ContextoDados>(registroJson, options)!;

        if (contextoRegistrado == null)
            return;

        RegistroAmigos = contextoRegistrado.RegistroAmigos;
        RegistroCaixas = contextoRegistrado.RegistroCaixas;
        RegistroEmprestimos = contextoRegistrado.RegistroEmprestimos;
        RegistroMultas = contextoRegistrado.RegistroMultas;
        RegistroReservas = contextoRegistrado.RegistroReservas;
        RegistroRevistas = contextoRegistrado.RegistroRevistas;
    }
}
