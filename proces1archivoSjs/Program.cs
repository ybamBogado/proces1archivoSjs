public class Proceso
{
    public string Nombre { get; set; }
    public int Prioridad { get; set; }
    public int Tiempo { get; set; }

    public Proceso(string nombre, int tiempo, int prioridad)
    {
        this.Nombre = nombre;
        this.Tiempo = tiempo;
        this.Prioridad = prioridad;
    }

    public override string ToString()
    {
        return $"{Nombre} Tiempo: {Tiempo} Prioridad: {Prioridad}";
    }
}

public class Program
{
    public static List<Proceso> ConvertirCsvAProcesos(string rutaArchivo)
    {
        var procesos = new List<Proceso>();
        var lineas = File.ReadAllLines(rutaArchivo);

        foreach (var linea in lineas)
        {
            var partes = linea.Split(';');
            if (partes.Length == 3)
            {
                var nombre = partes[0];
                var tiempo = int.Parse(partes[1]);
                var prioridad = int.Parse(partes[2]);
                var proceso = new Proceso(nombre, tiempo, prioridad);
                procesos.Add(proceso);
            }
        }

        return procesos;
    }

    public static void Main()
    {
        string rutaArchivo = "C:\\Users\\User\\source\\repos\\proces1archivoSjs\\proces1archivoSjs\\dataset.csv";
        var procesos = ConvertirCsvAProcesos(rutaArchivo);

        foreach (var proceso in procesos)
        {
            Console.WriteLine(proceso);
        }
    }
}