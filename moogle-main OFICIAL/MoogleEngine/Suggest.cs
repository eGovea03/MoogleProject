namespace MoogleEngine;

public class Suggestion
{
    public static string Suggest = "";//Una cadena vacia
    public static int index = 0;
   
   
    public int Levenshtein(string palabraUno, string palabraDos) 
    {
        char[] charPalabra1 = palabraUno.ToCharArray();
        char[] charPalabra2 = palabraDos.ToCharArray();
        
        //Se obtienen los valores de las longitudes de las palabras
        int valorUno= palabraUno.Length + 1;
        int valorDos = palabraDos.Length + 1;
        
        //esta matriz es creada para almacenar los valores de ,las distancias del Levenshtein
        int[,] Levenshtein = new int[valorDos,valorUno];
        for(int i = 0; i < valorUno; i++)
        {
            Levenshtein[0,i] = i;
        }
        for(int i = 0; i < valorDos; i++)
        {
            Levenshtein[i, 0] = i;
        }
        for (int i = 1; i < valorDos; i++)
        {
            for(int j = 1; j < valorUno; j++)
            {
                if(charPalabra2[i-1] == charPalabra1[j - 1])
                {
                    Levenshtein[i,j] = Math.Min(Math.Min(Levenshtein[i - 1, j - 1], Levenshtein[i, j - 1]), Levenshtein[i - 1, j]);
                }
                else
                {
                    Levenshtein[i,j] = Math.Min(Math.Min(Levenshtein[i-1,j-1] + 1, Levenshtein[i,j-1] + 1), Levenshtein[i-1,j] + 1);
                }
            }
        }
        // aqui se debe devolver el valor de la ultima celda(aij) de la matriz
        return Levenshtein[valorDos-1,valorUno-1];
    }
    public Suggestion(){
      foreach(int aux in Busqueda.Sugerencia)
        {
            if(aux == 0)
            {
                //Si el valor actual es 0 , se llama a la funcion SuggestWord con el valor que tiene el Query en la posicion del index
                Busqueda.Query[index] = Sugiere_Palabra(Busqueda.Query[index]);
            }
            //Aqui se le agrega el valor actual de Query a la variable Suggest
            Suggest += Busqueda.Query[index] + " ";
            index++;
        }
      index = 0;// esto en si lo que quiere decir es que se reinicia el valor del index
    }

    public string Sugiere_Palabra(string palabra)// conciste en sugerir una palabra similar a la recibida
    {
        string suggest = "";
        long distancia = 5000000;
        foreach(string palabraDos in GetFile.PALABRAS)
        {
   // La siguiente funcion se usa para calcular la distancia entre dos palabras
            int aux = Levenshtein(palabra, palabraDos);
           
            if(aux < distancia) { 
                distancia = aux; 
                suggest = palabraDos;
            }
        }
        //Se devuelve la palabra sugerida
        return suggest;
    }

}
