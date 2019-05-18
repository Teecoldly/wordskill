Imports System.Data.SqlClient
Module connect
    Dim con As New SqlConnection("Data Source=(local);Initial Catalog=Maratthon;Integrated Security=True")
    Friend dt As New DataTable
    Dim com As SqlCommand
    Dim da As SqlDataAdapter
    Sub concheck()
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
    End Sub
    Friend Function commandsql(ByVal sql As String)
        Try
            concheck()
            con.Open()

            com = New SqlCommand(sql, con)
            If com.ExecuteNonQuery() Then
                con.Close()
                Return True
            End If

        Catch ex As SqlException
            MsgBox(ex.ToString())
        End Try
        con.Close()
        Return False
    End Function

    Friend Function readsql(ByVal sql As String)
        Try
            concheck()
            con.Open()
            dt = New DataTable
            dt.Clear()
            MsgBox(sql)
            da = New SqlDataAdapter(sql, con)
            da.Fill(dt)
            con.Close()
            Return dt
        Catch ex As SqlException
            MsgBox(ex.ToString())
        End Try
        Return dt
    End Function
    Friend Sub watermarker(ByRef textbox As TextBox, ByVal textshow As String)
        If textbox.Text = "" Then
            textbox.ForeColor = Color.Silver
            textbox.Font = New Font("Microsoft Sans Serif", 8, Drawing.FontStyle.Italic)
            textbox.Text = textshow
        ElseIf textbox.Text <> vbNullString Then
            textbox.Clear()
            textbox.ForeColor = Color.Black
            textbox.Font = New Font("Microsoft Sans Serif", 8)


        End If
    End Sub
    Sub timespans(ByVal timestart As DateTime, ByVal timestop As DateTime, ByVal labelname As Label)

        Dim val As TimeSpan = timestart.Subtract(timestop)
        labelname.Text = val.Days & " days " & val.Hours & " hours and " & val.Minutes & " minutes until the race starts"

    End Sub
    Friend Sub watermarkerlave(ByRef textbox As TextBox, ByVal textshow As String)
        If textbox.Text = "" Then
            textbox.ForeColor = Color.Silver
            textbox.Font = New Font("Microsoft Sans Serif", 8, Drawing.FontStyle.Italic)
            textbox.Text = textshow
        ElseIf textbox.Text <> "" Then
            textbox.ForeColor = Color.Black
            textbox.Font = New Font("Microsoft Sans Serif", 8)
        Else

        End If
    End Sub

End Module
