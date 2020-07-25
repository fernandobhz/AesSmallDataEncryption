Imports System.Security.Cryptography
Imports System.IO

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Dim A As String = "Ola Mundo!"

        Dim M As Byte() = Encrypt("xxxx", System.Text.Encoding.Unicode.GetBytes(A))

        Dim B As String = System.Text.Encoding.Unicode.GetString(Decrypt("xxx", M))

        MsgBox(B)

    End Sub


End Class


Public Module AesSmallCrypt

    Private Function BuildPasswd(Passwd As String) As Byte()
        Dim DefaultPassword = "MOV EABX MOV EEX NULL CALL EBX STD CALL"

        Dim Passwd256Bits As String = String.Concat(Passwd, DefaultPassword).Substring(0, 32)

        Return System.Text.Encoding.ASCII.GetBytes(Passwd256Bits)

    End Function

    Private Function BuildIV(Passwd As String) As Byte()
        Dim DefaultPassword = "NULL CALL EBX STD CALL MOV EABX MOV EEX"

        Dim IV128Bits As String = String.Concat(Passwd, DefaultPassword).Substring(0, 16)

        Return System.Text.Encoding.ASCII.GetBytes(IV128Bits)

    End Function



    Public Function Encrypt(Passwd As String, Data As Byte()) As Byte()

        Dim AES As New AesCryptoServiceProvider
        AES.Key = BuildPasswd(Passwd)
        AES.IV = BuildIV(Passwd)

        Dim MS As New MemoryStream

        Using cStream As New CryptoStream(MS, AES.CreateEncryptor, CryptoStreamMode.Write)
            cStream.Write(Data, 0, Data.Length)
        End Using

        Return MS.ToArray

    End Function

    Public Function Decrypt(Passwd As String, Data As Byte()) As Byte()

        Dim AES As New AesCryptoServiceProvider
        AES.Key = BuildPasswd(Passwd)
        AES.IV = BuildIV(Passwd)

        Dim MS As New MemoryStream(Data)

        Dim Result As New MemoryStream
        Using dStream As New CryptoStream(MS, AES.CreateDecryptor, CryptoStreamMode.Read)
            dStream.CopyTo(Result)
        End Using

        Return Result.ToArray

    End Function

End Module