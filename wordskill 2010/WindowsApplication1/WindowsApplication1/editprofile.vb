Imports System.Text.RegularExpressions
Public Class editprofile
    Sub loaddfcombo() 'โหลดข้อมูลจาก Database to combox
        readsql("select * from Gender") ' load gender
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1 Step +1
                ComboBox1.Items.Add(dt.Rows(i).Item("Gender"))

            Next
        End If
        readsql("select CountryName from Country") ' loadcountry
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1 Step +1
                ComboBox2.Items.Add(dt.Rows(i).Item("CountryName"))

            Next
        End If
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
        login.Show()
        login.emaillogin = ""
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        manurunner.Show()

    End Sub



    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        timespans("#2019/07/21 06:00#", Date.Now, Label8)
    End Sub

    Private Sub editprofile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        email.Text = login.emaillogin
        loaddfcombo()
        watermarker(TextBox3, "Password")
        watermarker(TextBox4, "Re-enter password")

        Try
            readsql("SELECT u.firstname , u.lastname,r.Gender,r.DateOfBirth,c.CountryName FROM userinfo as  u  INNER JOIN Runner as r on u.Email = r.Email INNER JOIN  Country as c on r.CountryCode = c.CountryCode where u.email= '" & email.Text & "'")
            TextBox1.Text = dt.Rows(0).Item("firstname")
            TextBox2.Text = dt.Rows(0).Item("lastname")

            ComboBox1.Text = dt.Rows(0).Item("Gender")
            DateTimePicker1.Value = CDate(dt.Rows(0).Item("DateOfBirth")).ToString("dd-MM-yyyy")
            ComboBox2.Text = dt.Rows(0).Item("CountryName")

        Catch ex As Exception
            MsgBox("error loaddata")
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        Dim ages As TimeSpan = Now.Subtract(DateTimePicker1.Value)
        If TextBox3.Text <> "Password" Then
            Dim upper As String = "[A-Z]"
            Dim dgital As String = "[0-9]"
            Dim symbols As String = "[!@#$%^]"
            If TextBox3.TextLength < 6 Then
                MsgBox("Password least 6 characters ")
                TextBox3.Focus()
            ElseIf Regex.IsMatch(TextBox3.Text, upper) = False Then
                MsgBox("Password least 1 uppercase letter ")
                TextBox3.Focus()
            ElseIf Regex.IsMatch(TextBox3.Text, dgital) = False Then
                MsgBox("Password least 1 number/digit ")
                TextBox3.Focus()
            ElseIf Regex.IsMatch(TextBox3.Text, symbols) = False Then
                MsgBox("Password least 1 of the following symbols: !@#$%^ ")
                TextBox3.Focus()
            ElseIf TextBox4.Text = "Re-enter password" Then
                MsgBox("Plase input password agian")
                TextBox4.Focus()
            ElseIf TextBox3.Text <> TextBox4.Text Then
                MsgBox("Password not math password again")
                TextBox4.Focus()
            Else
                Try
                    If commandsql("UPDATE userinfo  set Password ='" & TextBox3.Text & "'  WHERE Email='" & email.Text & "'") = True Then
                        TextBox3.Clear()
                        TextBox4.Clear()
                        watermarker(TextBox3, "Password")
                        TextBox3.UseSystemPasswordChar = False
                        watermarker(TextBox4, "Re-enter password")
                        TextBox4.UseSystemPasswordChar = False
                    End If

                Catch ex As Exception
                    MsgBox("Chage password error")
                End Try
            End If
        End If
        If TextBox1.Text = "First name" Then
            MsgBox("Plase input First name")
            TextBox1.Focus()
        ElseIf TextBox2.Text = "Last name" Then
            MsgBox("Plase input Last name")
            TextBox2.Focus()
        ElseIf ages.Days < 3650 Then
            MsgBox("Date of Birth least 10 years old at the time of registration")
            DateTimePicker1.Focus()
        Else
            Dim county As String
            readsql("select CountryCode from Country where CountryName ='" & ComboBox2.Text & "'")
            county = dt.Rows(0).Item(0)
            Try
                If commandsql("UPDATE userinfo SET FirstName='" & TextBox1.Text & "',LastName='" & TextBox2.Text & "' WHERE Email='" & email.Text & "'") = True Then

                    If commandsql("UPDATE Runner SET  Gender='" & ComboBox1.Text & "',DateOfBirth='" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "',CountryCode='" & county & "'  WHERE Email='" & email.Text & "'") = True Then
                        MsgBox("Save Success")

                    End If
                End If
            Catch ex As Exception
                MsgBox("Update runner error ")
            End Try
        End If
    End Sub

    Private Sub TextBox3_Enter(sender As Object, e As EventArgs) Handles TextBox3.Enter
        watermarker(TextBox3, "Password")
        TextBox3.UseSystemPasswordChar = True
    End Sub

    Private Sub TextBox4_Enter(sender As Object, e As EventArgs) Handles TextBox4.Enter
        watermarker(TextBox4, "Re-enter password")
        TextBox4.UseSystemPasswordChar = True
    End Sub

    Private Sub TextBox3_Leave(sender As Object, e As EventArgs) Handles TextBox3.Leave
        watermarkerlave(TextBox3, "Password")

        If TextBox3.Text = "Password" Then
            TextBox3.UseSystemPasswordChar = False

        Else
            TextBox3.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub TextBox4_Leave(sender As Object, e As EventArgs) Handles TextBox4.Leave
        watermarkerlave(TextBox4, "Re-enter password")
        If TextBox4.Text = "Re-enter password" Then
            TextBox4.UseSystemPasswordChar = False

        Else
            TextBox4.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave
        watermarkerlave(TextBox1, "First name")
    End Sub

    Private Sub TextBox1_Enter(sender As Object, e As EventArgs) Handles TextBox1.Enter
        watermarker(TextBox1, "First name")
    End Sub

    Private Sub TextBox2_Enter(sender As Object, e As EventArgs) Handles TextBox2.Enter
        watermarker(TextBox2, "Last name")
    End Sub

    Private Sub TextBox2_Leave(sender As Object, e As EventArgs) Handles TextBox2.Leave
        watermarkerlave(TextBox2, "Last name")
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        TextBox3.Clear()
        TextBox4.Clear()
        watermarker(TextBox3, "Password")
        TextBox3.UseSystemPasswordChar = False
        watermarker(TextBox4, "Re-enter password")
        TextBox4.UseSystemPasswordChar = False
    End Sub
End Class