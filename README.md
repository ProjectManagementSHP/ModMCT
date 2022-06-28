**MLF Software**
*Documentacion*

Para los distintos modulos, al momento de validar a los distintos usuarios y determinar cuales son los privilegios con los cuales cuentan, 
se creo una variable llamada Opcion, en la cual son la siguientes:

```Visual Basic
Select Case dep
    Case "Desarrollo"
        With OpcionesLog
            .ShowDialog()
        End With
        Principal.Text = "Desarrollo"
        Principal.Show()
        Me.Hide()
    Case "Almacen"
        opcion = 2
        insertMLFNotification()
        Principal.Text = "Almacen"
        Principal.Show()
        Me.Hide()
    Case "Corte"
        opcion = 1
        insertMLFNotification()
        Principal.Text = "Corte"
        Principal.Show()
        Me.Hide()
    Case "Aplicadores"
        opcion = 3
        insertMLFNotification()
        Principal.Text = "Aplicadores"
        Principal.Show()
        Me.Hide()
    Case "XP"
        opcion = 4
        insertMLFNotification()
        Principal.Text = "XP"
        Principal.Show()
        Me.Hide()
    Case "Compras"
        opcion = 5
        insertMLFNotification()
        Principal.Text = "Compras"
        Principal.Show()
        Me.Hide()
    Case "PlanCorte"
        opcion = 6
        insertMLFNotification()
        Principal.Text = "Planeacion Corte"
        Principal.Show()
        Me.Hide()
    Case "PlanXP"
        opcion = 7
        insertMLFNotification()
        Principal.Text = "Planeacion XP"
        Principal.Show()
        Me.Hide()
    Case Else
        MsgBox("Tu departamento no tiene acceso a este modulo, verificalo e intenta de nuevo")
End Select
```
Sin embargo, en el caso de desarrollo y direccion, se creo una forma en la cual la opcion la escogen ellos para ingresar, se despliega la forma llamada
OpcionesLog.

Tambien, cabe mencionar que se puede acceder con la clave del administrador, la cual es:
User: admin
Password: Supremadmin

Se tiene la opcion de inicio de sesion automatica, la cual al provenir del Dash Board, este ya trae los privilegios previos para entrar.
Nota: Se puede cambiar de usuario en todo momento.

*Notificaciones*

Para las notificaciones, se agrega a la tabla (si es que no existe registro de usuario en ella), tblMLFNotifications
Los cuales los codigos de notificaciones son los siguientes:
1 = solicitado (almacen, aplicadores)
2 = solicitado con corto (almacen, aplicadores)
3 = confirmado por almacen (corte)
4 = confirmado por aplicadores (corte)
5 = listo para entrar (corte)
6 = hold por almacen (corte, compras)
7 = hold por aplicadores (corte)
8 = nueva fecha promesa de compras (almacen)
9 = Desviacion rechazada (aplicadores)
10 = Se desvio una terminal con exito (corte, almacen, compras)

Si no ha recibido la notificacion, permanecera en 1 el campo SendReceive, de lo contrario cambiara a 0

*Actualizaciones*

El manejo de notificacion de actualizaciones se maneja bajo la libreria FileSystemWatcher, la cual vigilamos la carpeta de Application Files de donde esta el instalador.
Codigo:
```Visual Basic
Public Sub AutoUpdate()
        FSW.Path = "\\10.17.182.22\sea-s\MLF\Application Files"
        FSW.IncludeSubdirectories = True
        FSW.EnableRaisingEvents = True
End Sub
Private Sub FSW_Created(sender As Object, e As IO.FileSystemEventArgs) Handles FSW.Created
        Dim limpiaChar As String = e.Name, extraVersion As String = ""
        limpiaChar = LTrim(RTrim(limpiaChar))
        limpiaChar = limpiaChar.Replace("_", ".")
        For i As Integer = 0 To limpiaChar.Length - 1
            If IsNumeric(limpiaChar(i)) Or limpiaChar(i) = "." Then
                extraVersion += limpiaChar(i)
            End If
        Next
        ver = extraVersion
        ActualizacionRequerida.BringToFront()
        ActualizacionRequerida.ShowDialog()
        If flagActualizacion = 1 Then
            Application.Exit()
        End If
End Sub
```
Evento AutoUpdate llamado desde el load de la forma.

*Reordenamiento de CWO*

El reordenamiento de los CWO, se manejo con clases para usar los metodos como objetos, la clase se llama AutomaticSort, se hace uso del algoritmo de ordenamiento burbuja
y para verificar siempre existencia de numeros consecutivos, se uso recursividad, todo lo relacionado a la clase AutomaticSort se utiliza al momento de sacar de fila un CWO,
cambiar su prioridad o cuando recien lo solicitan.
