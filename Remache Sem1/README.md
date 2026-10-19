# Actividad MAUI - Cálculo Aporte IESS

Proyecto .NET MAUI que calcula el aporte mensual al IESS (9.45%) a partir del salario ingresado.

Contenido del repositorio:

- Código fuente del proyecto MAUI (Remache Sem1)
- Views/v_inicio.xaml(.cs): Interfaz con 4 Entry (Nombre, Apellido, Edad, Salario) y botón de cálculo
- AppShell configurado para abrir v_inicio como página inicial

Cómo ejecutar (entorno local):

1. Abrir la solución `Remache Sem1.slnx` en Visual Studio 2022/2026 con .NET 10 y las cargas de trabajo de MAUI instaladas.
2. Seleccionar la plataforma (Windows/Android/iOS) y ejecutar (F5) o reiniciar depuración (Ctrl+Shift+F5).

Formato de entrega solicitado por la asignatura:
- Documento PDF con explicación del funcionamiento, capturas de la aplicación y enlace al repositorio en GitHub.

Instrucciones rápidas para subir al repositorio remoto (usar el script push_to_github.ps1):

1. Abre PowerShell en la carpeta raíz del proyecto (donde está este README).
2. Ejecuta:

   .\push_to_github.ps1 -RemoteUrl "https://github.com/lfremache/Actividad-MAUI.git" -Branch main

   Nota: el push solicitará autenticación si tu Git no tiene credenciales configuradas.

Capturas:
- Añade en la carpeta `docs/` o `captures/` las imágenes y actualiza este README si quieres que aparezcan dentro del PDF.

Licencia: libre para uso académico.
