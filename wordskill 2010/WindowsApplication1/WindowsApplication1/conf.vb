Public Class conf
    Dim runneid As Integer
    Dim registerid As Integer
    Sub loadrunerid()
        readsql("select RunnerId as id from Runner where email='" & register.TextBox1.Text & "'")
        runneid = dt.Rows(0).Item("id")
    End Sub
    Sub loadregisterid()
        readsql("SELECT RegistrationId as regid  FROM Registration where RunnerId=" & runneid & "")
        registerid = dt.Rows(0).Item("regid")
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        If commandsql("INSERT INTO Userinfo(Email, Password, FirstName, LastName, RoleId) VALUES ('" & register.TextBox1.Text & "','" & register.TextBox2.Text & "','" & register.TextBox4.Text & "','" & register.TextBox5.Text & "','R')") Then
            If commandsql("INSERT INTO Runner (Email,Gender,DateOfBirth, CountryCode) VALUES ('" & register.TextBox1.Text & "','" & register.ComboBox1.Text & "','" & register.DateTimePicker1.Value.ToString("yyyy-MM-dd") & "','" & register.county & "')") Then
                loadrunerid()
                If commandsql("INSERT INTO Registration( RunnerId, RegistrationDateTime, RaceKitOptionId, RegistrationStatusId, Cost, CharityId, SponsorshipTarget) VALUES ('" & runneid & "','" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "','" & regevent.optionkin & "',1," & regevent.total & ",7,0)") Then
                    loadregisterid()
                    If regevent.CheckBox1.Checked = True Then
                        Try
                            commandsql("INSERT INTO RegistrationEvent( RegistrationId, EventId) VALUES (" & registerid & ",'15_5FM')")
                        Catch ex As Exception
                            MsgBox("error insert event 1 ")
                        End Try
                    End If
                    If regevent.CheckBox2.Checked = True Then
                        Try
                            commandsql("INSERT INTO RegistrationEvent( RegistrationId, EventId) VALUES (" & registerid & ",'15_5HM')")
                        Catch ex As Exception
                            MsgBox("error insert event 2 ")
                        End Try
                    End If
                    If regevent.CheckBox3.Checked = True Then
                        Try
                            commandsql("INSERT INTO RegistrationEvent( RegistrationId, EventId) VALUES (" & registerid & ",'15_5FR')")
                        Catch ex As Exception
                            MsgBox("error insert event 3 ")
                        End Try
                    End If
                    login.Show()
                    Me.Close()
                    regevent.Close()
                    register.Close()
                Else
                    MsgBox("error insert Registration")
                End If

            Else
                MsgBox("ERROR insert runner")
            End If
        Else
            MsgBox("ERROR insert user")
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        timespans("#2019/07/21 06:00#", Date.Now, Label8)
    End Sub

    Private Sub conf_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub
End Class