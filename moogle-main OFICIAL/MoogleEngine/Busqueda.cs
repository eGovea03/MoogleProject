using System.Text.RegularExpressions;
namespace MoogleEngine;

public class Busqueda
{
   public static string? query;
   public static List<string> Query = new List<string>();
   public static List<int> TF_Query = Enumerable.Repeat(0, GetFile.TOTALpalabras).ToList();
   // Se declara una lista de enteros llamada TF_Query, inicializada con ceros y con una longitud igual a la cantidad total de palabras obtenidas en GetFile
   public static List<int> Docs_Palabras = Enumerable.Repeat(0, GetFile.TOTALpalabras).ToList();
   public static List<DataBase> OrdenDocs = new List<DataBase>();
   public static List<int> Sugerencia = new List<int>();
   int IndexDoc = 0;
   int snippetwords = 50;//cantidad de palabras que apareceran como resultado de la busqueda

    public Busqueda(){
            if (query != null)
            {
                 // Si el valor de la Query es nula ,se crea una lista de strings llamada Query, que se obtiene al dividir la variable query en palabras utilizando una expresión regular
                Query = new List<string>(Regex.Split(query, @"\W+"));
            }
            foreach (string word in Query)
            {
                int aux = GetFile.PALABRAS.IndexOf(word);//IndexOf se utiliza para buscar la 1ra aparicion de un caracter dentro de una cadena
                if (aux != -1)
                {
                    TF_Query[aux] = 1;
                    Sugerencia.Add(1);
                }
                else
                {
                Sugerencia.Add(0);
                }
            }
            foreach (int numero in TF_Query)
            {
                foreach (DataBase doc in GetFile.Documentos)
                {
                    if (numero == 1)
                    {
                         // Se cuenta la cantidad de veces que aparece la palabra correspondiente en el documento actual , se cuenta la cantidad de veces que aparece la palabra correspondiente en el documento actual
                        double cantidad = doc.Words.Count(s => s == GetFile.PALABRAS[IndexDoc]);
                        if (cantidad != 0)
                        {
                            Docs_Palabras[IndexDoc] += 1;
                            doc.TF.Add(1 + Math.Log10(cantidad)); 
                        }
                        else
                        {
                            doc.TF.Add(0);
                        }
                    }
                    else
                    {
                        doc.TF.Add(0);
                    }
                }
                IndexDoc++;
            }
            foreach (DataBase doc in GetFile.Documentos)
            {
                int index = 0;
                float score = 0;
                foreach (double tf in doc.TF)
                {
                    doc.TFIDF.Add(doc.TF[index] * Math.Log10((double)GetFile.CantidadDoc / (1 + Docs_Palabras[index])));  
                    score += (float)doc.TFIDF[index];
                    index++;
                }
                doc.Score = score;
                double max = doc.TFIDF.Max();
            if (max != 0)
            {
                string mayorTFIDF = GetFile.PALABRAS[(doc.TFIDF.IndexOf(max))];
                doc.Snippet = NearWords(doc.Content, mayorTFIDF, snippetwords);
            }
               
            }
            // Este metodo OrderByDescending se utiliza para ordenar una coleccion de elmentos en orden descendente
            OrdenDocs = GetFile.Documentos.OrderByDescending(a => a.Score).ToList();  
           
            OrdenDocs.RemoveAll(a => a.Score <= 0);
    }
    public static string NearWords(string entrada, string word, int number) 
    {
        Match match = Regex.Match(entrada, @$"\W{word}\W");
        int index =match.Index; 
        List<string> start = entrada.Substring(0, index).Split(' ').ToList();
        start.Reverse();
        int aux = Math.Min(number, start.Count);
        start.RemoveRange(aux-1, start.Count- aux);
        start.Reverse();
        string startstring = string.Join(" ", start);
        List<string> final = entrada.Substring(index).Split(' ').ToList();
        aux = Math.Min(number, final.Count);
        final.RemoveRange(aux - 1, final.Count - aux);
        string finWord = string.Join(" ", final);
        string result = startstring + finWord;
      
      
        return result;
    }
}