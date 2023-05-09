namespace MoogleEngine;

public class DataBase
{
    public DataBase(string path, string title, string content, string[] words, List<double>TF, List<double>TFIDF, string snippet, float score)
    {
        // A continuacion se establecen propiedades a partir de parametros del constructor
        this.TF = TF;
        this.TFIDF = TFIDF;
        this.Path = path;
        this.Content = content;
        this.Title = title;
        this.Snippet = snippet;
        this.Score = score;
        this.Words = words;

    }
    // A continuacion se definen unas propiedades publicas de tipo cadena con modificadores para acceder(set)
    public List<double> TF { get; private set; }
    public List<double> TFIDF { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public string Path { get; private set; }
    public string Snippet { get; set; }
    public float Score { get; set; }
    public string[] Words { get; private set; }
}
 
