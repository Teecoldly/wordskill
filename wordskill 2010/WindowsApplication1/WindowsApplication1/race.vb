Public Class race
    Private raceages As String
    Public sql As Integer
    Public dtevent As DataTable

    Sub setraceages()
        If login.ages <= 17 Then
            raceages = "Under 18"
            sql = 1
        ElseIf login.ages <= 29 Then
            raceages = "18 to 29"
            sql = 2
        ElseIf login.ages <= 39 Then
            raceages = "30 to 39"
            sql = 3
        ElseIf login.ages <= 55 Then
            raceages = "40 to 55"
            sql = 4
        ElseIf login.ages <= 70 Then
            raceages = "56 to 70"
            sql = 5
        Else
            sql = 6
            raceages = "Over 70"
        End If
        age.Text = raceages
        sex.Text = login.sex
        readsql("SELECT re.EventId,re.RaceTime FROM RegistrationEvent as re INNER JOIN  Registration as rg on re.RegistrationId = rg.RegistrationId inner JOIN Runner  as  r on rg.RunnerId = r.RunnerId WHERE r.email ='" & login.emaillogin & "'")
        dtevent = New DataTable
        dtevent.Clear()
        dtevent = dt
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
        timespans("#2019/07/21 06:00#", Date.Now, Label8)
    End Sub

    Private Sub race_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        Timer1.Start()
        setraceages()
        If sql = 1 Then
            sqlbw("<", 18)
        ElseIf sql = 2 Then
            sqlbw(18, 29)
        ElseIf sql = 3 Then
            sqlbw(30, 39)
        ElseIf sql = 4 Then
            sqlbw(40, 55)
        ElseIf sql = 5 Then
            sqlbw(56, 70)
        ElseIf sql = 6 Then
            sqlbw(">", 70)
        End If
        DataGridView1.ClearSelection()
    End Sub
    Sub sqlbw(ByVal value1 As Integer, ByVal value2 As Integer)
        Try
            For i As Integer = 0 To dtevent.Rows.Count - 1 Step +1
                readsql("SELECT  (  cast( m.YearHeld as varchar  ) +''+c.CountryName)as county,e.EventTypeId,re.RaceTime ,year(getdate())-year(r.DateOfBirth) as ages FROM RegistrationEvent as re inner JOIN Event as e on re.eventid = e.EventId inner JOIN Marathon as m on e.MarathonId = m.MarathonId INNER JOIN Country as c on m.CountryCode = c.CountryCode inner JOIN Registration as rs on re.RegistrationId = rs.RegistrationId inner join Runner as r on r.RunnerId = rs.RunnerId WHERE year(getdate())-year(r.DateOfBirth) BETWEEN " & value1 & " and   " & value2 & " and r.Gender='" & login.sex & "' and re.EventId='" & dtevent.Rows(i).Item(0) & "'  and  re.RaceTime is not null  ORDER BY re.RaceTime asc ")
                Dim rankcate As Integer = 0
                Dim rankall As Integer = 0
                For a As Integer = 0 To dt.Rows.Count - 1 Step +1
                    If dtevent.Rows(i).Item(1) >= dt.Rows(a).Item(2) Then
                        rankcate += 1
                    End If
                Next
                readsql("SELECT  (  cast( m.YearHeld as varchar  ) +''+c.CountryName)as county,e.EventTypeId,re.RaceTime ,year(getdate())-year(r.DateOfBirth) as ages FROM RegistrationEvent as re inner JOIN Event as e on re.eventid = e.EventId inner JOIN Marathon as m on e.MarathonId = m.MarathonId INNER JOIN Country as c on m.CountryCode = c.CountryCode inner JOIN Registration as rs on re.RegistrationId = rs.RegistrationId inner join Runner as r on r.RunnerId = rs.RunnerId WHERE  re.EventId='" & dtevent.Rows(i).Item(0) & "'  and  re.RaceTime is not null ORDER BY re.RaceTime asc ")

                For a As Integer = 0 To dt.Rows.Count - 1 Step +1
                    If dtevent.Rows(i).Item(1) >= dt.Rows(a).Item(2) Then
                        rankall += 1
                    End If
                Next
                Dim formatrun As String = "#ERROR"
                Dim timerace As TimeSpan = TimeSpan.FromSeconds(dtevent.Rows(i).Item(1))
                If dt.Rows(i).Item(1) = "FR" Then
                    formatrun = "5km Fun Run"
                ElseIf dt.Rows(i).Item(1) = "FM" Then
                    formatrun = "42km Full marathon"
                ElseIf dt.Rows(i).Item(1) = "HM" Then
                    formatrun = "21km Half Marathon"
                End If
                DataGridView1.Rows.Add(dt.Rows(i).Item(0), formatrun, timerace.Hours & "h " & timerace.Minutes.ToString.PadLeft(2, "0"c) & "m " & timerace.Seconds.ToString.PadLeft(2, "0"c) & "s", "#" & rankall, "#" & rankcate)


            Next
        Catch ex As Exception

        End Try


    End Sub
    Sub sqlbw(ByVal format As String, ByVal value As Integer)
        Try
            For i As Integer = 0 To dtevent.Rows.Count - 1 Step +1
                readsql("SELECT  (cast( m.YearHeld as varchar  )+''+c.CountryName)as county,e.EventTypeId,re.RaceTime ,year(getdate())-year(r.DateOfBirth) as ages FROM RegistrationEvent as re inner JOIN Event as e on re.eventid = e.EventId inner JOIN Marathon as m on e.MarathonId = m.MarathonId INNER JOIN Country as c on m.CountryCode = c.CountryCode inner JOIN Registration as rs on re.RegistrationId = rs.RegistrationId inner join Runner as r on r.RunnerId = rs.RunnerId WHERE year(getdate())-year(r.DateOfBirth)  " & format & value & " and r.Gender='" & login.sex & "' and re.EventId='" & dtevent.Rows(i).Item(0) & "'  and  re.RaceTime is not null  ORDER BY re.RaceTime asc ")
                Dim rankcate As Integer = 0
                Dim rankall As Integer = 0
                For a As Integer = 0 To dt.Rows.Count - 1 Step +1
                    If dtevent.Rows(i).Item(1) >= dt.Rows(a).Item(2) Then
                        rankcate += 1
                    End If
                Next
                readsql("SELECT (cast( m.YearHeld as varchar  )+''+c.CountryName) as county,e.EventTypeId,re.RaceTime ,year(getdate())-year(r.DateOfBirth) as ages FROM RegistrationEvent as re inner JOIN Event as e on re.eventid = e.EventId inner JOIN Marathon as m on e.MarathonId = m.MarathonId INNER JOIN Country as c on m.CountryCode = c.CountryCode inner JOIN Registration as rs on re.RegistrationId = rs.RegistrationId inner join Runner as r on r.RunnerId = rs.RunnerId WHERE  re.EventId='" & dtevent.Rows(i).Item(0) & "'   and  re.RaceTime is not null  ORDER BY re.RaceTime asc ")

                For a As Integer = 0 To dt.Rows.Count - 1 Step +1
                    If dtevent.Rows(i).Item(1) >= dt.Rows(a).Item(2) Then
                        rankall += 1
                    End If
                Next
                Dim formatrun As String = "#ERROR"

                Dim timerace As TimeSpan = TimeSpan.FromSeconds(dtevent.Rows(i).Item(1))
                If dt.Rows(i).Item(1) = "FR" Then
                    formatrun = "5km Fun Run"
                ElseIf dt.Rows(i).Item(1) = "FM" Then
                    formatrun = "42km Full marathon"
                ElseIf dt.Rows(i).Item(1) = "HM" Then
                    formatrun = "21km Half Marathon"
                End If
                DataGridView1.Rows.Add(dt.Rows(i).Item(0), formatrun, timerace.Hours & "h " & timerace.Minutes.ToString.PadLeft(2, "0"c) & "m " & timerace.Seconds.ToString.PadLeft(2, "0"c) & "s", "#" & rankall, "#" & rankcate)


            Next
        Catch ex As Exception

        End Try


    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        manurunner.Show()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        login.Show()

    End Sub
End Class