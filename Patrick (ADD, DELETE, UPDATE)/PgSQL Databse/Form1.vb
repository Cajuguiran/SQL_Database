Imports System.Data
Imports Npgsql

Public Class Form1
    Dim lv As ListViewItem
    Dim selected As String

    Private Sub ClearTextbox()
        Me.txtnumber.Text = ""
        Me.txtlastname.Text = ""
        Me.txtfirstname.Text = ""
        Me.txtmiddle.Text = ""
        Me.txtaddress.Text = ""
        Me.txtcontact.Text = ""
        Me.cmbcourse.Text = ""
        Me.cmbgender.Text = ""

    End Sub

    Private Sub ExecutedNoQuery(query As String)
        openCon()
        cmd = New NpgsqlCommand(query, cn)
        cmd.ExecuteNonQuery()
        cn.Close()

    End Sub
    Private Sub PopListView()
        ListView1.Clear()

        With ListView1
            .View = View.Details
            .GridLines = True
            .Columns.Add("ID", 50)
            .Columns.Add("Last Name", 100)
            .Columns.Add("First Name", 100)
            .Columns.Add("Middle", 50)
            .Columns.Add("Course", 100)
            .Columns.Add("Address", 180)
            .Columns.Add("Gender", 100)
            .Columns.Add("Contact", 100)

        End With

        openCon()
        sql = "Select * from tblstudinfo"
        cmd = New NpgsqlCommand(sql, cn)
        dr = cmd.ExecuteReader()

        Do While dr.Read() = True
            lv = New ListViewItem(dr("studno").ToString)
            lv.SubItems.Add(dr("studlastname"))
            lv.SubItems.Add(dr("studefirstname"))
            lv.SubItems.Add(dr("studmiddle"))
            lv.SubItems.Add(dr("studcourse"))
            lv.SubItems.Add(dr("studaddress"))
            lv.SubItems.Add(dr("studgender"))
            lv.SubItems.Add(dr("studcontact"))
            ListView1.Items.Add(lv)
        Loop
        cn.Close()

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopListView()

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If MsgBox("Are you sure to save this record?", vbQuestion + vbYesNo) = vbYes Then
            openCon()
            sql = "INSERT INTO tblstudinfo (studno, studlastname, studefirstname, studmiddle, studaddress, studgender, studcontact, studcourse)" _
            & "VALUES ('" & (Me.txtnumber.Text) & "', '" & (Me.txtlastname.Text) & "', '" & (Me.txtfirstname.Text) & "', '" & (Me.txtmiddle.Text) & "', '" & (Me.txtaddress.Text) & "', '" & (Me.cmbgender.Text) & "', '" & (Me.txtcontact.Text) & "', '" & (Me.cmbcourse.Text) & "')"
            cmd = New NpgsqlCommand(sql, cn)
            cmd.ExecuteNonQuery()
            cn.Close()
        End If
        PopListView()
        ClearTextbox()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        Dim i As Integer
        For i = 0 To ListView1.SelectedItems.Count - 1

            selected = ListView1.SelectedItems(i).Text
            openCon()
            sql = "Select * from tblstudinfo where studno = '" & selected & "'"
            cmd = New NpgsqlCommand(sql, cn)
            dr = cmd.ExecuteReader

            dr.Read()
            Me.txtnumber.Text = dr("studno")
            Me.txtlastname.Text = dr("studlastname")
            Me.txtfirstname.Text = dr("studefirstname")
            Me.txtmiddle.Text = dr("studmiddle")
            Me.txtaddress.Text = dr("studaddress")
            Me.cmbgender.Text = dr("studgender")
            Me.txtcontact.Text = dr("studcontact")
            Me.cmbcourse.Text = dr("studcourse")
            cn.Close()
        Next
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If MsgBox("Are you sure to update this record?", vbExclamation + vbYesNo) = vbYes Then
            openCon()
            sql = "UPDATE tblstudinfo SET studno = '" & (Me.txtnumber.Text) & "' , studlastname = '" & (Me.txtlastname.Text) & "', studefirstname = '" & (Me.txtfirstname.Text) & "', studmiddle = '" & (Me.txtmiddle.Text) & "', studaddress =  '" & (Me.txtaddress.Text) & "', studgender = '" & (Me.cmbgender.Text) & "', studcontact =  '" & (Me.txtcontact.Text) & "', studcourse = '" & (Me.cmbcourse.Text) & "' where studno = '" & selected & "'"
            cmd = New NpgsqlCommand(sql, cn)
            cmd.ExecuteNonQuery()
            cn.Close()
            PopListView()
            ClearTextbox()
        End If

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If MsgBox("Are you sure to delete this record?", vbExclamation + vbYesNo) = vbYes Then
            Dim query As String = "DELETE FROM tblstudinfo WHERE studno = '" & (txtnumber.Text) & "'"
            ExecutedNoQuery(query)
            PopListView()
            ClearTextbox()
        End If
    End Sub

    Private Sub txtnumber_TextChanged(sender As Object, e As EventArgs) Handles txtnumber.TextChanged

    End Sub
End Class

