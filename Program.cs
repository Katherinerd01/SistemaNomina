using System;

public abstract class Empleado
{
    public string PrimerNombre { get; set; }
    public string ApellidoPaterno { get; set; }
    public string NumeroSeguroSocial { get; set; }

    public Empleado(string primerNombre, string apellidoPaterno, string numeroSeguroSocial)
    {
        PrimerNombre = primerNombre;
        ApellidoPaterno = apellidoPaterno;
        NumeroSeguroSocial = numeroSeguroSocial;
    }

    public abstract decimal CalcularIngreso();

    public override string ToString()
    {
        return $"{PrimerNombre} {ApellidoPaterno}\nNúmero de seguro social: {NumeroSeguroSocial}";
    }
}

public class EmpleadoAsalariado : Empleado
{
    public decimal SalarioSemanal { get; set; }

    public EmpleadoAsalariado(string primerNombre, string apellidoPaterno, string numeroSeguroSocial, decimal salarioSemanal)
        : base(primerNombre, apellidoPaterno, numeroSeguroSocial)
    {
        SalarioSemanal = salarioSemanal;
    }

    public override decimal CalcularIngreso()
    {
        return SalarioSemanal;
    }

    public override string ToString()
    {
        return $"Empleado asalariado: {base.ToString()}\nSalario semanal: {SalarioSemanal:C}";
    }
}

public class EmpleadoPorHoras : Empleado
{
    public decimal SueldoPorHora { get; set; }
    public decimal HorasTrabajadas { get; set; }

    public EmpleadoPorHoras(string primerNombre, string apellidoPaterno, string numeroSeguroSocial, decimal sueldoPorHora, decimal horasTrabajadas)
        : base(primerNombre, apellidoPaterno, numeroSeguroSocial)
    {
        SueldoPorHora = sueldoPorHora;
        HorasTrabajadas = horasTrabajadas;
    }

    public override decimal CalcularIngreso()
    {
        if (HorasTrabajadas <= 40)
        {
            return SueldoPorHora * HorasTrabajadas;
        }
        else
        {
            return (40 * SueldoPorHora) + ((HorasTrabajadas - 40) * SueldoPorHora * 1.5M);
        }
    }

    public override string ToString()
    {
        return $"Empleado por horas: {base.ToString()}\nSueldo por hora: {SueldoPorHora:C}\nHoras trabajadas: {HorasTrabajadas:F2}";
    }
}

public class EmpleadoPorComision : Empleado
{
    public decimal VentasBrutas { get; set; }
    public decimal TarifaComision { get; set; }

    public EmpleadoPorComision(string primerNombre, string apellidoPaterno, string numeroSeguroSocial, decimal ventasBrutas, decimal tarifaComision)
        : base(primerNombre, apellidoPaterno, numeroSeguroSocial)
    {
        VentasBrutas = ventasBrutas;
        TarifaComision = tarifaComision;
    }

    public override decimal CalcularIngreso()
    {
        return VentasBrutas * TarifaComision;
    }

    public override string ToString()
    {
        return $"Empleado por comisión: {base.ToString()}\nVentas brutas: {VentasBrutas:C}\nTarifa comisión: {TarifaComision:P2}";
    }
}

public class EmpleadoBaseMasComision : EmpleadoPorComision
{
    public decimal SalarioBase { get; set; }

    public EmpleadoBaseMasComision(string primerNombre, string apellidoPaterno, string numeroSeguroSocial, decimal ventasBrutas, decimal tarifaComision, decimal salarioBase)
        : base(primerNombre, apellidoPaterno, numeroSeguroSocial, ventasBrutas, tarifaComision)
    {
        SalarioBase = salarioBase;
    }

    public override decimal CalcularIngreso()
    {
        return (VentasBrutas * TarifaComision) + (SalarioBase * 1.1M);
    }

    public override string ToString()
    {
        return $"Empleado base más comisión: {base.ToString()}\nSalario base: {SalarioBase:C}";
    }
}

class Program
{
    static void Main()
    {
        Empleado[] empleados = new Empleado[]
        {
            new EmpleadoAsalariado("Juan", "Pérez", "123-45-6789", 800.00M),
            new EmpleadoPorHoras("María", "González", "987-65-4321", 16.75M, 42.0M),
            new EmpleadoPorComision("Carlos", "López", "111-22-3333", 10000.00M, 0.06M),
            new EmpleadoBaseMasComision("Ana", "Martínez", "444-55-6666", 5000.00M, 0.04M, 300.00M),
            new EmpleadoBaseMasComision("Rosa", "Mora", "666-22-7777", 6000.00M, 0.04M, 200.00M),
        };

        foreach (Empleado empleado in empleados)
        {
            Console.WriteLine(empleado);
            Console.WriteLine($"Ingreso: {empleado.CalcularIngreso():C}\n");
        }
    }
}

