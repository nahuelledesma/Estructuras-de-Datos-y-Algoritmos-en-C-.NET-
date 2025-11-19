# DataStructures
Proyecto educativo en C# que contiene implementaciones clásicas de estructuras de datos y algoritmos.
Descripción
-----------
Proyecto pequeño para demostrar y practicar implementaciones de estructuras de datos (listas enlazadas, pilas, colas, árboles, tablas hash, etc.) y algoritmos de ordenamiento y búsqueda.
Estado y objetivos
------------------
- Solución: `dataStructures.slnx`
- Proyecto principal: `DataStructures/DataStructures.csproj` (target: `net10.0`)
- Contenido: ejemplos y estructuras en `Structures/`, algoritmos en `Algorithms/`.
Requisitos
---------
- .NET SDK (versión compatible con `net10.0`).
- Windows / PowerShell para los ejemplos presentados (los comandos `dotnet` funcionan igualmente en otros entornos).
Estructura del repositorio
--------------------------
- `dataStructures.slnx` — solución principal.
- `DataStructures/` — proyecto C# con el ejecutable y `Program.cs`.
- `DataStructures/Algorithms/Searching.cs` — algoritmos de búsqueda.
- `DataStructures/Algorithms/Sorting.cs` — algoritmos de ordenamiento.
- `DataStructures/Structures/` — implementaciones de estructuras de datos (`LinkedList.cs`, `Stack.cs`, `Queue.cs`, `BinaryTree.cs`, `Hash.cs`, ...).
Compilar y ejecutar
-------------------
Abre PowerShell en la raíz del repositorio y ejecuta:

```powershell
dotnet build .\dataStructures.slnx
dotnet run --project .\DataStructures\DataStructures.csproj
```

O bien, moverse al directorio del proyecto y ejecutar:

```powershell
cd .\DataStructures
dotnet run
```

Notas de uso
------------
- `Program.cs` contiene el punto de entrada y ejemplos de uso. Puedes modificarlo para probar otras estructuras o algoritmos.
- Añade nuevas clases dentro de `Structures/` o `Algorithms/` y referencia desde `Program.cs` para demostraciones.

Contribuir
---------
- Crea una rama por cada cambio: `feature/<nombre>`.
- Abre pull requests con una descripción clara y ejemplos de uso.
- Añade pruebas o ejemplos en `Program.cs` si agregas nuevas funcionalidades.

Licencia
--------
Licencia no especificada. Añade un archivo `LICENSE` si quieres publicar este repositorio bajo una licencia concreta (por ejemplo MIT).

Contacto
-------
Para preguntas o mejoras, crea un issue en el repositorio o contáctame directamente.
