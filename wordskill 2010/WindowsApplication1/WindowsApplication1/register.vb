Imports System.Text.RegularExpressions
Public Class register
    Public county As String


    Sub loaddfcombo() 'โหลดข้อมูลจาก Database to combox
        readsql("select * from Gender") ' load gender
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1 Step +1
                ComboBox1.Items.Add(dt.Rows(i).Item("gender"))

            Next
        End If
        readsql("select countryname from Country") ' loadcountry
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1 Step +1
                ComboBox2.Items.Add(dt.Rows(i).Item("countryname"))

            Next
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        timespans("#2019/07/21 06:00#", Date.Now, Label8)
    End Sub

    Private Sub register_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        watermarker(TextBox1, "Email Address")
        watermarker(TextBox2, "Password")
        watermarker(TextBox3, "Re-enter password")
        watermarker(TextBox4, "First name")
        watermarker(TextBox5, "Last name")
        Timer1.Start()
        loaddfcombo()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close() 'ปิดแบบ obj ไม่คงเหลือ
        login.Show() 'กลับไปที่หน้า login
        If Application.OpenForms().OfType(Of regevent).Any Then
            regevent.Close()
        End If
    End Sub

    Private Sub TextBox1_Enter(sender As Object, e As EventArgs) Handles TextBox1.Enter
        watermarker(TextBox1, "Email Address")

    End Sub

    Private Sub TextBox2_Enter(sender As Object, e As EventArgs) Handles TextBox2.Enter
        watermarker(TextBox2, "Password")
        TextBox2.UseSystemPasswordChar = True
    End Sub

    Private Sub TextBox3_Enter(sender As Object, e As EventArgs) Handles TextBox3.Enter
        watermarker(TextBox3, "Re-enter password")
        TextBox3.UseSystemPasswordChar = True
    End Sub

    Private Sub TextBox4_Enter(sender As Object, e As EventArgs) Handles TextBox4.Enter
        watermarker(TextBox4, "First name")
    End Sub
    Private Sub TextBox5_Enter(sender As Object, e As EventArgs) Handles TextBox5.Enter
        watermarker(TextBox5, "Last name")
    End Sub

    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave

        watermarkerlave(TextBox1, "Email Address")
    End Sub

    Private Sub TextBox2_Leave(sender As Object, e As EventArgs) Handles TextBox2.Leave
        watermarkerlave(TextBox2, "Password")

        If TextBox2.Text = "Password" Then
            TextBox2.UseSystemPasswordChar = False

        Else
            TextBox2.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub TextBox3_Leave(sender As Object, e As EventArgs) Handles TextBox3.Leave
        watermarkerlave(TextBox3, "Re-enter password")
        If TextBox3.Text = "Re-enter password" Then
            TextBox3.UseSystemPasswordChar = False

        Else
            TextBox3.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub TextBox4_Leave(sender As Object, e As EventArgs) Handles TextBox4.Leave
        watermarkerlave(TextBox4, "First name")
    End Sub
    Private Sub TextBox5_Leave(sender As Object, e As EventArgs) Handles TextBox5.Leave
        watermarkerlave(TextBox5, "Last name")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim emailformat As String = "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"
        Dim upper As String = "[A-Z]"
        Dim dgital As String = "[0-9]"
        Dim symbols As String = "[!@#$%^]"
        Dim ages As TimeSpan = Now.Subtract(DateTimePicker1.Value)
        Try
            readsql("select * from userinfo where email = '" & TextBox1.Text & "' ")
            If TextBox1.Text = "Email Address" Then
                MsgBox("Plase Enter email")
                TextBox1.Focus()
            ElseIf dt.Rows.Count > 0 Then
                MsgBox("Already have data in the system!")
                TextBox1.Focus()
            ElseIf Regex.IsMatch(TextBox1.Text, emailformat) = False Then
                MsgBox("Email format error ")
                TextBox1.Focus()
            ElseIf TextBox2.TextLength < 6 Then
                MsgBox("Password least 6 characters ")
                TextBox2.Focus()
            ElseIf Regex.IsMatch(TextBox2.Text, upper) = False Then
                MsgBox("Password least 1 uppercase letter ")
                TextBox2.Focus()
            ElseIf Regex.IsMatch(TextBox2.Text, dgital) = False Then
                MsgBox("Password least 1 number/digit ")
                TextBox2.Focus()
            ElseIf Regex.IsMatch(TextBox2.Text, symbols) = False Then
                MsgBox("Password least 1 of the following symbols: !@#$%^ ")
                TextBox2.Focus()
            ElseIf TextBox3.Text = "Re-enter password" Then
                MsgBox("Plase input password agian")
                TextBox3.Focus()
            ElseIf TextBox3.Text <> TextBox2.Text Then
                MsgBox("Password not math password again")
                TextBox3.Focus()
            ElseIf TextBox4.Text = "First name" Then
                MsgBox("Plase input first name ")
                TextBox4.Focus()
            ElseIf TextBox5.Text = "Last name" Then
                MsgBox("Plase input Last name ")
                TextBox5.Focus()
            ElseIf ComboBox1.SelectedIndex = -1 Then
                MsgBox("Plase select gender")
                ComboBox1.Focus()
            ElseIf ages.Days < 3650 Then
                MsgBox("Date of Birth least 10 years old at the time of registration")
                DateTimePicker1.Focus()

            ElseIf ComboBox2.SelectedIndex = -1 Then
                MsgBox("Plase select county")
                ComboBox2.Focus()
            Else
                readsql("select CountryCode from Country where CountryName ='" & ComboBox2.Text & "'")
                county = dt.Rows(0).Item(0)
                Me.Hide()
                regevent.Show()


            End If
        Catch ex As Exception
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            TextBox5.Clear()

            ComboBox1.SelectedIndex = -1
            ComboBox2.SelectedIndex = -1
            DateTimePicker1.Value = Now
            watermarker(TextBox1, "Email Address")
            watermarker(TextBox2, "Password")
            If TextBox2.Text = "Password" Then
                TextBox2.UseSystemPasswordChar = False

            Else
                TextBox2.UseSystemPasswordChar = True
            End If

            watermarker(TextBox3, "Re-enter password")
            If TextBox3.Text = "Re-enter password" Then
                TextBox3.UseSystemPasswordChar = False

            Else
                TextBox3.UseSystemPasswordChar = True
            End If
            watermarker(TextBox4, "First name")
            watermarker(TextBox5, "Last name")
            MsgBox("ERROR! Sytex Plase input again")
        End Try


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()

        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = -1
        DateTimePicker1.Value = Now
        watermarker(TextBox1, "Email Address")
        watermarker(TextBox2, "Password")
        If TextBox2.Text = "Password" Then
            TextBox2.UseSystemPasswordChar = False

        Else
            TextBox2.UseSystemPasswordChar = True
        End If

        watermarker(TextBox3, "Re-enter password")
        If TextBox3.Text = "Re-enter password" Then
            TextBox3.UseSystemPasswordChar = False

        Else
            TextBox3.UseSystemPasswordChar = True
        End If

        watermarker(TextBox4, "First name")
        watermarker(TextBox5, "Last name")

    End Sub
End Class