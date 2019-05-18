Imports System.Text.RegularExpressions

Public Class login
    Public emaillogin As String
    Public sex As String
    Public ages As String
    Sub loaddatainfo()
        readsql("SELECT Gender,DateOfBirth FROM Runner  where email = '" & emaillogin & "'")
        sex = dt.Rows(0).Item("Gender")

        ages = Year(Now) - Year(dt.Rows(0).Item("DateOfBirth"))

    End Sub
    Sub textboxautomask()
        watermarker(TextBox1, "Enter your email Address")
        watermarker(TextBox2, "Enter your Password")
    End Sub
    Private Sub login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        textboxautomask()
        Timer1.Start()
    End Sub





    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave

        watermarkerlave(TextBox1, "Enter your email Address")
    End Sub

    Private Sub TextBox2_Leave(sender As Object, e As EventArgs) Handles TextBox2.Leave
        watermarkerlave(TextBox2, "Enter your Password")
        If TextBox2.Text = "Enter your Password" Then
            TextBox2.UseSystemPasswordChar = False
        Else
            TextBox2.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Clear()
        TextBox2.Clear()
        textboxautomask()
        TextBox2.UseSystemPasswordChar = False
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        timespans("#2019/07/21 06:00#", Date.Now, Label8)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim sqlinject As String = "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"
        If TextBox1.Text = "Enter your email Address" Then
            MsgBox("Plase enter  Email")
            TextBox1.Focus()

        ElseIf Regex.IsMatch(TextBox1.Text, sqlinject) = False Then
            MsgBox("Plase enter format Email")
            TextBox1.Clear()
        ElseIf TextBox2.Text = "Enter your Password" Then
            MsgBox("Plase enter password")
            TextBox2.Focus()
        Else

            Dim sql As String = "select Email,RoleId from userinfo where Email='" & TextBox1.Text & "' and Password='" & TextBox2.Text & "'"

            Try
                readsql(sql)
                If dt.Rows.Count > 0 Then

                    If dt.Rows(0).Item("RoleId") = "R" Then
                        emaillogin = dt.Rows(0).Item("Email")
                        loaddatainfo()

                        manurunner.Show()
                        TextBox1.Clear()
                        TextBox2.Clear()
                        textboxautomask()
                        TextBox2.UseSystemPasswordChar = False
                        Me.Hide()
                    Else
                        MsgBox("Program is Runner Only")
                        TextBox1.Clear()
                        TextBox2.Clear()
                        textboxautomask()
                        TextBox2.UseSystemPasswordChar = False
                    End If
                Else
                    textboxautomask()
                    MsgBox("Email or password wrong!")
                End If
            Catch ex As Exception
                TextBox1.Clear()
                TextBox2.Clear()
                textboxautomask()
                TextBox2.UseSystemPasswordChar = False
                Me.Hide()
                MsgBox("ERROR")
            End Try

        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox1.Clear()
        TextBox2.Clear()
        textboxautomask()
        TextBox2.UseSystemPasswordChar = False
        Me.Hide()
        register.Show()
    End Sub

    Private Sub TextBox1_Enter(sender As Object, e As EventArgs) Handles TextBox1.Enter


        watermarker(TextBox1, "Enter your email Address")

    End Sub

    Private Sub TextBox2_Enter(sender As Object, e As EventArgs) Handles TextBox2.Enter
        watermarker(TextBox2, "Enter your Password")
        TextBox2.UseSystemPasswordChar = True
    End Sub
End Class