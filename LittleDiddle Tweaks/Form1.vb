Imports System.Drawing.Text
Imports System.ServiceProcess
Imports Microsoft.Win32

Public Class Form1
    Private Sub closeButton_Click(sender As Object, e As EventArgs) Handles closeButton.Click
        Form1.ActiveForm.Close()
    End Sub

    Private Sub minimizeButton_Click(sender As Object, e As EventArgs) Handles minimizeButton.Click
        Form1.ActiveForm.WindowState = FormWindowState.Minimized
    End Sub

    ' form drag stuff
    Private isDragging As Boolean = False
    Private startPoint As Point

    Private Sub Form1_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If e.Button = MouseButtons.Left Then
            isDragging = True
            startPoint = New Point(e.X, e.Y)
        End If
    End Sub

    Private Sub Form1_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If isDragging Then
            Dim p As Point = PointToScreen(e.Location)
            Me.Location = New Point(p.X - startPoint.X, p.Y - startPoint.Y)
        End If
    End Sub

    Private Sub Form1_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        If e.Button = MouseButtons.Left Then
            isDragging = False
        End If
    End Sub
    'drag stuff ends here

    Private Sub uppButton_Click(sender As Object, e As EventArgs) Handles uppButton.Click
        Try
            Dim psi As New ProcessStartInfo("powercfg")
            psi.Arguments = "-duplicatescheme e9a42b02-d5df-448d-aa00-03f14749eb61"
            psi.Verb = "runas"
            psi.WindowStyle = ProcessWindowStyle.Hidden
            Process.Start(psi).WaitForExit()

            psi.Arguments = "-setactive e9a42b02-d5df-448d-aa00-03f14749eb61"
            Process.Start(psi).WaitForExit()

            MessageBox.Show("Ultimate Performance power plan enabled!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub performanceVisualsButton_Click(sender As Object, e As EventArgs) Handles performanceVisualsButton.Click
        Try
            Dim key As String = "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\VisualEffects"
            Registry.SetValue(key, "VisualFXSetting", 2, RegistryValueKind.DWord)

            key = "HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics"

            MessageBox.Show("Visual effects set to best performance!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub disableCoreParkingButton_Click(sender As Object, e As EventArgs) Handles disableCoreParkingButton.Click
        Try
            Dim keyPath As String = "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Power\PowerSettings\54533251-82be-4824-96c1-47b60b740d00"
            Registry.SetValue(keyPath & "\0cc5b647-c1df-4637-891a-dec35c318583", "ValueMax", 0, RegistryValueKind.DWord)
            Registry.SetValue(keyPath & "\0cc5b647-c1df-4637-891a-dec35c318583", "ValueMin", 0, RegistryValueKind.DWord)

            MessageBox.Show("CPU core parking disabled!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub fortnitePriorityButton_Click(sender As Object, e As EventArgs) Handles fortnitePriorityButton.Click
        Try
            Dim procs() As Process = Process.GetProcessesByName("FortniteClient-Win64-Shipping")
            If procs.Length = 0 Then
                MessageBox.Show("Fortnite is not running!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            For Each p As Process In procs
                p.PriorityClass = ProcessPriorityClass.High
            Next

            MessageBox.Show("Fortnite process priority set to High!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub gpuSchedulingButton_Click(sender As Object, e As EventArgs) Handles gpuSchedulingButton.Click
        Try
            Dim key As String = "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"
            Registry.SetValue(key, "HwSchMode", 2, RegistryValueKind.DWord)

            MessageBox.Show("Hardware-accelerated GPU scheduling enabled! Restart required for changes to take effect.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub enableGameModeButton_Click(sender As Object, e As EventArgs) Handles enableGameModeButton.Click
        Try
            Dim key As String = "HKEY_CURRENT_USER\Software\Microsoft\GameBar"
            Registry.SetValue(key, "AllowAutoGameMode", 1, RegistryValueKind.DWord)

            MessageBox.Show("Windows Game Mode enabled!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub disableFullScreenOptimzationsButton_Click(sender As Object, e As EventArgs) Handles disableFullScreenOptimzationsButton.Click
        Try
            Dim key As String = "HKEY_CURRENT_USER\System\GameConfigStore"
            Registry.SetValue(key, "GameDVR_FSEBehaviorMode", 2, RegistryValueKind.DWord)

            MessageBox.Show("Fullscreen optimizations disabled!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub highGpuPreferenceButton_Click(sender As Object, e As EventArgs) Handles highGpuPreferenceButton.Click
        Try
            Dim key As String = "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\DirectX\GraphicsPreferences"
            Registry.SetValue(key, "GpuPreference", 2, RegistryValueKind.DWord)
            MessageBox.Show("High-performance GPU preference enabled! Restart may be required.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub clearTempFilesButton_Click(sender As Object, e As EventArgs) Handles clearTempFilesButton.Click
        Try
            Dim tempPath As String = IO.Path.GetTempPath()
            Dim tempDir As New IO.DirectoryInfo(tempPath)

            For Each file As IO.FileInfo In tempDir.GetFiles()
                Try
                    file.Delete()
                Catch
                End Try
            Next

            For Each dir As IO.DirectoryInfo In tempDir.GetDirectories()
                Try
                    dir.Delete(True)
                Catch
                End Try
            Next

            MessageBox.Show("Temporary files cleared!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub virtualMemAdjustmentButton_Click(sender As Object, e As EventArgs) Handles virtualMemAdjustmentButton.Click
        Try
            Dim psi As New ProcessStartInfo("wmic")
            psi.Arguments = "computersystem where name='%computername%' set AutomaticManagedPagefile=True"
            psi.Verb = "runas"
            psi.WindowStyle = ProcessWindowStyle.Hidden
            Process.Start(psi).WaitForExit()

            MessageBox.Show("Pagefile set to system managed size.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub disableUnnescessaryIndexingButton_Click(sender As Object, e As EventArgs) Handles disableUnnescessaryIndexingButton.Click
        Try
            Dim svc As New ServiceController("WSearch")
            If svc.Status = ServiceControllerStatus.Running Then
                svc.Stop()
                svc.WaitForStatus(ServiceControllerStatus.Stopped)
                MessageBox.Show("Windows Search indexing disabled!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Windows Search service is already stopped.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub flushDnsButton_Click(sender As Object, e As EventArgs) Handles flushDnsButton.Click
        Try
            Dim psi As New ProcessStartInfo("ipconfig")
            psi.Arguments = "/flushdns"
            psi.Verb = "runas"
            psi.WindowStyle = ProcessWindowStyle.Hidden
            Process.Start(psi).WaitForExit()

            MessageBox.Show("DNS cache flushed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub networkTweaksButton_Click(sender As Object, e As EventArgs) Handles networkTweaksButton.Click
        Try
            Dim psi As New ProcessStartInfo("netsh")
            psi.Arguments = "interface tcp set global autotuninglevel=normal"
            psi.Verb = "runas"
            psi.WindowStyle = ProcessWindowStyle.Hidden
            Process.Start(psi).WaitForExit()

            psi.Arguments = "interface tcp set global chimney=enabled"
            Process.Start(psi).WaitForExit()

            MessageBox.Show("Safe TCP network optimizations applied!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub disableUnnecessaryServices_Click(sender As Object, e As EventArgs) Handles disableUnnecessaryServices.Click
        Try
            Dim svc As New ServiceController("Spooler")
            If svc.Status = ServiceControllerStatus.Running Then
                svc.Stop()
                svc.WaitForStatus(ServiceControllerStatus.Stopped)
            End If

            svc = New ServiceController("MapsBroker")
            If svc.Status = ServiceControllerStatus.Running Then
                svc.Stop()
                svc.WaitForStatus(ServiceControllerStatus.Stopped)
            End If

            MessageBox.Show("Selected Windows services disabled!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub disableXboxGamebarButton_Click(sender As Object, e As EventArgs) Handles disableXboxGamebarButton.Click
        Try
            Dim key1 As String = "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\GameDVR"
            Registry.SetValue(key1, "AppCaptureEnabled", 0, RegistryValueKind.DWord)

            Dim key2 As String = "HKEY_CURRENT_USER\System\GameConfigStore"
            Registry.SetValue(key2, "GameDVR_Enabled", 0, RegistryValueKind.DWord)

            MessageBox.Show("Xbox Game Bar / Game DVR disabled!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub disableDynamicTickButton_Click(sender As Object, e As EventArgs) Handles disableDynamicTickButton.Click
        If MessageBox.Show("This is risky and may destabilize your system. Continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Try
                Dim psi As New ProcessStartInfo("bcdedit")
                psi.Arguments = "/set disabledynamictick yes"
                psi.Verb = "runas"
                psi.WindowStyle = ProcessWindowStyle.Hidden
                Process.Start(psi).WaitForExit()

                MessageBox.Show("Dynamic tick disabled! Restart required.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub disableMemoryCompression_Click(sender As Object, e As EventArgs) Handles disableMemoryCompression.Click
        If MessageBox.Show("This may cause instability if you run out of memory. Continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Try
                Dim psi As New ProcessStartInfo("powershell")
                psi.Arguments = "-Command ""Disable-MMAgent -mc"""
                psi.Verb = "runas"
                psi.WindowStyle = ProcessWindowStyle.Hidden
                Process.Start(psi).WaitForExit()

                MessageBox.Show("Memory compression disabled! Restart may be required.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub disableSecurityButton_Click(sender As Object, e As EventArgs) Handles disableSecurityButton.Click
        If MessageBox.Show("Disabling security is HIGHLY risky. Continue at your own risk?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Try
                Dim key As String = "HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\DataCollection"
                Registry.SetValue(key, "AllowTelemetry", 0, RegistryValueKind.DWord)

                MessageBox.Show("Telemetry disabled! Some security features may be affected.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub disableThrottleButton_Click(sender As Object, e As EventArgs) Handles disableThrottleButton.Click
        If MessageBox.Show("This will disable all CPU/GPU throttling and may overheat your system. Continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Try
                Dim psi As New ProcessStartInfo("powercfg")
                psi.Arguments = "-setactive e9a42b02-d5df-448d-aa00-03f14749eb61"
                psi.Verb = "runas"
                psi.WindowStyle = ProcessWindowStyle.Hidden
                Process.Start(psi).WaitForExit()

                MessageBox.Show("Hardware maxed! Monitor temps carefully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim wmi As New System.Management.ManagementClass("SystemRestore")
            Dim inParams As System.Management.ManagementBaseObject = wmi.GetMethodParameters("CreateRestorePoint")

            inParams("Description") = "TweakApp Restore Point"
            inParams("RestorePointType") = 0 ' APPLICATION_INSTALL
            inParams("EventType") = 100 ' BEGIN_SYSTEM_CHANGE

            wmi.InvokeMethod("CreateRestorePoint", inParams, Nothing)

            MessageBox.Show("System restore point created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error creating restore point: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
