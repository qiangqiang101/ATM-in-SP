Imports System.Drawing
Imports GTA
Imports INMNativeUI

Public Module ControlEvents

    Public Sub ControlEventsWaypoint()
        For Each button As UIResRectangle In wpPool
            If UIMenu.IsMouseInBounds(button.Position, button.Size) Then
                hoveredButton = button
                ChangeCursorPointer(CursorType.Fuck)
            Else
                Select Case Game.Player.Character.Name
                    Case Game.GetGXTEntry("ACCNA_FRANKLIN")
                        button.Color = FranklinColor
                    Case Game.GetGXTEntry("ACCNA_MIKE")
                        button.Color = MichaelColor
                    Case Game.GetGXTEntry("ACCNA_TREVOR")
                        button.Color = TrevorColor
                End Select
                ChangeCursorPointer(CursorType.Normal)
            End If
        Next

        If Game.IsControlPressed(0, GTA.Control.PhoneSelect) Then
            GetNearestATM.SetWaypoint
        End If

        If hoveredButton IsNot Nothing Then
            Select Case Game.Player.Character.Name
                Case Game.GetGXTEntry("ACCNA_FRANKLIN")
                    hoveredButton.Color = FranklinColorH
                Case Game.GetGXTEntry("ACCNA_MIKE")
                    hoveredButton.Color = MichaelColorH
                Case Game.GetGXTEntry("ACCNA_TREVOR")
                    hoveredButton.Color = TrevorColorH
            End Select
        End If
    End Sub

    Public Sub ControlEventsMain()
        For Each button As UIResRectangle In buttonPool
            If UIMenu.IsMouseInBounds(button.Position, button.Size) Then
                hoveredButton = button
            Else
                button.Color = Color.LightPink
            End If
        Next

        If Game.IsControlJustReleased(0, Control.PhoneUp) Then
            If hoveredButton Is btBalance Then
                hoveredButton = btWithdraw
            ElseIf hoveredButton Is btWithdraw Then
                hoveredButton = btDeposit
            ElseIf hoveredButton Is btDeposit Then
                hoveredButton = btTrans
            Else
                hoveredButton = btBalance
            End If
        End If

        If Game.IsControlJustReleased(0, Control.PhoneDown) Then
            If hoveredButton Is btBalance Then
                hoveredButton = btTrans
            ElseIf hoveredButton Is btWithdraw Then
                hoveredButton = btBalance
            ElseIf hoveredButton Is btDeposit Then
                hoveredButton = btWithdraw
            Else
                hoveredButton = btDeposit
            End If
        End If

        If Game.IsControlPressed(0, GTA.Control.PhoneSelect) Then
            selectedButton = hoveredButton
        Else
            selectedButton = Nothing
        End If

        If hoveredButton IsNot Nothing Then hoveredButton.Color = Color.Crimson
    End Sub

    Public Sub ControlEventsTransfer()
        For Each button As UIResRectangle In buttonPool2
            If UIMenu.IsMouseInBounds(button.Position, button.Size) Then
                hoveredButton = button
            Else
                button.Color = Color.LightPink
            End If
        Next

        If Game.IsControlJustReleased(0, Control.PhoneUp) Then
            If hoveredButton Is btFranklin Then
                hoveredButton = btTrevor
            ElseIf hoveredButton Is btMichael Then
                hoveredButton = btFranklin
            Else
                hoveredButton = btMichael
            End If
        End If

        If Game.IsControlJustReleased(0, Control.PhoneDown) Then
            If hoveredButton Is btFranklin Then
                hoveredButton = btMichael
            ElseIf hoveredButton Is btMichael Then
                hoveredButton = btTrevor
            Else
                hoveredButton = btFranklin
            End If
        End If

        If Game.IsControlPressed(0, GTA.Control.PhoneSelect) Then
            selectedButton = hoveredButton
        Else
            selectedButton = Nothing
        End If

        If hoveredButton IsNot Nothing Then hoveredButton.Color = Color.Crimson
    End Sub

    Public Sub ControlEventsDeposit()
        For Each button As UIResRectangle In dPool
            If UIMenu.IsMouseInBounds(button.Position, button.Size) Then
                hoveredButton = button
            Else
                button.Color = Color.LightPink
            End If
        Next

        If Game.IsControlJustReleased(0, Control.PhoneUp) Then
            If hoveredButton Is bt1kd Then
                hoveredButton = btCustomd
            ElseIf hoveredButton Is bt5kd Then
                hoveredButton = bt1kd
            ElseIf hoveredButton Is bt10kd Then
                hoveredButton = bt5kd
            ElseIf hoveredButton Is bt50kd Then
                hoveredButton = bt10kd
            ElseIf hoveredButton Is bt100kd Then
                hoveredButton = bt50kd
            ElseIf hoveredButton Is bt500kd Then
                hoveredButton = bt100kd
            ElseIf hoveredButton Is bt1md Then
                hoveredButton = bt500kd
            ElseIf hoveredButton Is bt5md Then
                hoveredButton = bt1md
            ElseIf hoveredButton Is bt10md Then
                hoveredButton = bt5md
            ElseIf hoveredButton Is bt50md Then
                hoveredButton = bt10md
            ElseIf hoveredButton Is bt100md Then
                hoveredButton = bt50md
            ElseIf hoveredButton Is bt500md Then
                hoveredButton = bt100md
            ElseIf hoveredButton Is bt1bd Then
                hoveredButton = bt500md
            ElseIf hoveredButton Is btremaind Then
                hoveredButton = bt1bd
            ElseIf hoveredButton Is btcustomd Then
                hoveredButton = btRemaind
            End If
        End If

        If Game.IsControlJustReleased(0, Control.PhoneDown) Then
            If hoveredButton Is bt1kd Then
                hoveredButton = bt5kd
            ElseIf hoveredButton Is bt5kd Then
                hoveredButton = bt10kd
            ElseIf hoveredButton Is bt10kd Then
                hoveredButton = bt50kd
            ElseIf hoveredButton Is bt50kd Then
                hoveredButton = bt100kd
            ElseIf hoveredButton Is bt100kd Then
                hoveredButton = bt500kd
            ElseIf hoveredButton Is bt500kd Then
                hoveredButton = bt1md
            ElseIf hoveredButton Is bt1md Then
                hoveredButton = bt5md
            ElseIf hoveredButton Is bt5md Then
                hoveredButton = bt10md
            ElseIf hoveredButton Is bt10md Then
                hoveredButton = bt50md
            ElseIf hoveredButton Is bt50md Then
                hoveredButton = bt100md
            ElseIf hoveredButton Is bt100md Then
                hoveredButton = bt500md
            ElseIf hoveredButton Is bt500md Then
                hoveredButton = bt1bd
            ElseIf hoveredButton Is bt1bd Then
                hoveredButton = btRemaind
            ElseIf hoveredButton Is btRemaind Then
                hoveredButton = btCustomd
            ElseIf hoveredButton Is btCustomd Then
                hoveredButton = bt1kd
            End If
        End If

        If Game.IsControlPressed(0, GTA.Control.PhoneSelect) Then
            selectedButton = hoveredButton
        Else
            selectedButton = Nothing
        End If

        If hoveredButton IsNot Nothing Then hoveredButton.Color = Color.Crimson
    End Sub

    Public Sub ControlEventsWithdraw()
        For Each button As UIResRectangle In wPool
            If UIMenu.IsMouseInBounds(button.Position, button.Size) Then
                hoveredButton = button
            Else
                button.Color = Color.LightPink
            End If
        Next

        If Game.IsControlJustReleased(0, Control.PhoneUp) Then
            If hoveredButton Is bt1kw Then
                hoveredButton = btCustomw
            ElseIf hoveredButton Is bt5kw Then
                hoveredButton = bt1kw
            ElseIf hoveredButton Is bt10kw Then
                hoveredButton = bt5kw
            ElseIf hoveredButton Is bt50kw Then
                hoveredButton = bt10kw
            ElseIf hoveredButton Is bt100kw Then
                hoveredButton = bt50kw
            ElseIf hoveredButton Is bt500kw Then
                hoveredButton = bt100kw
            ElseIf hoveredButton Is bt1mw Then
                hoveredButton = bt500kw
            ElseIf hoveredButton Is bt5mw Then
                hoveredButton = bt1mw
            ElseIf hoveredButton Is bt10mw Then
                hoveredButton = bt5mw
            ElseIf hoveredButton Is bt50mw Then
                hoveredButton = bt10mw
            ElseIf hoveredButton Is bt100mw Then
                hoveredButton = bt50mw
            ElseIf hoveredButton Is bt500mw Then
                hoveredButton = bt100mw
            ElseIf hoveredButton Is bt1bw Then
                hoveredButton = bt500mw
            ElseIf hoveredButton Is btRemainw Then
                hoveredButton = bt1bw
            ElseIf hoveredButton Is btCustomw Then
                hoveredButton = btRemainw
            End If
        End If

        If Game.IsControlJustReleased(0, Control.PhoneDown) Then
            If hoveredButton Is bt1kw Then
                hoveredButton = bt5kw
            ElseIf hoveredButton Is bt5kw Then
                hoveredButton = bt10kw
            ElseIf hoveredButton Is bt10kw Then
                hoveredButton = bt50kw
            ElseIf hoveredButton Is bt50kw Then
                hoveredButton = bt100kw
            ElseIf hoveredButton Is bt100kw Then
                hoveredButton = bt500kw
            ElseIf hoveredButton Is bt500kw Then
                hoveredButton = bt1mw
            ElseIf hoveredButton Is bt1mw Then
                hoveredButton = bt5mw
            ElseIf hoveredButton Is bt5mw Then
                hoveredButton = bt10mw
            ElseIf hoveredButton Is bt10mw Then
                hoveredButton = bt50mw
            ElseIf hoveredButton Is bt50mw Then
                hoveredButton = bt100mw
            ElseIf hoveredButton Is bt100mw Then
                hoveredButton = bt500mw
            ElseIf hoveredButton Is bt500mw Then
                hoveredButton = bt1bw
            ElseIf hoveredButton Is bt1bw Then
                hoveredButton = btRemainw
            ElseIf hoveredButton Is btRemainw Then
                hoveredButton = btCustomw
            ElseIf hoveredButton Is btCustomw Then
                hoveredButton = bt1kw
            End If
        End If

        If Game.IsControlPressed(0, GTA.Control.PhoneSelect) Then
            selectedButton = hoveredButton
        Else
            selectedButton = Nothing
        End If

        If hoveredButton IsNot Nothing Then hoveredButton.Color = Color.Crimson
    End Sub

    Public Sub ControlEventsTransfer2()
        For Each button As UIResRectangle In tPool
            If UIMenu.IsMouseInBounds(button.Position, button.Size) Then
                hoveredButton = button
            Else
                button.Color = Color.LightPink
            End If
        Next

        If Game.IsControlJustReleased(0, Control.PhoneUp) Then
            If hoveredButton Is bt1kt Then
                hoveredButton = btCustomt
            ElseIf hoveredButton Is bt5kt Then
                hoveredButton = bt1kt
            ElseIf hoveredButton Is bt10kt Then
                hoveredButton = bt5kt
            ElseIf hoveredButton Is bt50kt Then
                hoveredButton = bt10kt
            ElseIf hoveredButton Is bt100kt Then
                hoveredButton = bt50kt
            ElseIf hoveredButton Is bt500kt Then
                hoveredButton = bt100kt
            ElseIf hoveredButton Is bt1mt Then
                hoveredButton = bt500kt
            ElseIf hoveredButton Is bt5mt Then
                hoveredButton = bt1mt
            ElseIf hoveredButton Is bt10mt Then
                hoveredButton = bt5mt
            ElseIf hoveredButton Is bt50mt Then
                hoveredButton = bt10mt
            ElseIf hoveredButton Is bt100mt Then
                hoveredButton = bt50mt
            ElseIf hoveredButton Is bt500mt Then
                hoveredButton = bt100mt
            ElseIf hoveredButton Is bt1bt Then
                hoveredButton = bt500mt
            ElseIf hoveredButton Is btRemaint Then
                hoveredButton = bt1bt
            ElseIf hoveredButton Is btCustomt Then
                hoveredButton = btRemaint
            End If
        End If

        If Game.IsControlJustReleased(0, Control.PhoneDown) Then
            If hoveredButton Is bt1kt Then
                hoveredButton = bt5kt
            ElseIf hoveredButton Is bt5kt Then
                hoveredButton = bt10kt
            ElseIf hoveredButton Is bt10kt Then
                hoveredButton = bt50kt
            ElseIf hoveredButton Is bt50kt Then
                hoveredButton = bt100kt
            ElseIf hoveredButton Is bt100kt Then
                hoveredButton = bt500kt
            ElseIf hoveredButton Is bt500kt Then
                hoveredButton = bt1mt
            ElseIf hoveredButton Is bt1mt Then
                hoveredButton = bt5mt
            ElseIf hoveredButton Is bt5mt Then
                hoveredButton = bt10mt
            ElseIf hoveredButton Is bt10mt Then
                hoveredButton = bt50mt
            ElseIf hoveredButton Is bt50mt Then
                hoveredButton = bt100mt
            ElseIf hoveredButton Is bt100mt Then
                hoveredButton = bt500mt
            ElseIf hoveredButton Is bt500mt Then
                hoveredButton = bt1bt
            ElseIf hoveredButton Is bt1bt Then
                hoveredButton = btRemaint
            ElseIf hoveredButton Is btRemaint Then
                hoveredButton = btCustomt
            ElseIf hoveredButton Is btCustomt Then
                hoveredButton = bt1kt
            End If
        End If

        If Game.IsControlPressed(0, GTA.Control.PhoneSelect) Then
            selectedButton = hoveredButton
        Else
            selectedButton = Nothing
        End If

        If hoveredButton IsNot Nothing Then hoveredButton.Color = Color.Crimson
    End Sub

    Public Sub ButtonEvents()
        If selectedButton Is btBalance Then
            _drawMenu = False
            _drawBalance = True
        ElseIf selectedButton Is btDeposit Then
            Script.Wait(200)
            If Set_Amount = "NULL" Then
                RequestAdditionTextFile("FMMC")
                Set_Amount = Game.GetGXTEntry("collision_68lkszv")
            End If
            _drawMenu = False
            _drawDeposit = True
            hoveredButton = btCustomd
        ElseIf selectedButton Is bt1kd Then
            DepositMoney(1000)
        ElseIf selectedButton Is bt5kd Then
            DepositMoney(5000)
        ElseIf selectedButton Is bt10kd Then
            DepositMoney(10000)
        ElseIf selectedButton Is bt50kd Then
            DepositMoney(50000)
        ElseIf selectedButton Is bt100kd Then
            DepositMoney(100000)
        ElseIf selectedButton Is bt500kd Then
            DepositMoney(500000)
        ElseIf selectedButton Is bt1md Then
            DepositMoney(1000000)
        ElseIf selectedButton Is bt5md Then
            DepositMoney(5000000)
        ElseIf selectedButton Is bt10md Then
            DepositMoney(10000000)
        ElseIf selectedButton Is bt50md Then
            DepositMoney(50000000)
        ElseIf selectedButton Is bt100md Then
            DepositMoney(100000000)
        ElseIf selectedButton Is bt500md Then
            DepositMoney(500000000)
        ElseIf selectedButton Is bt1bd Then
            DepositMoney(1000000000)
        ElseIf selectedButton Is btRemaind Then
            DepositMoney(GetDepositRemainingAmount)
        ElseIf selectedButton Is btCustomd Then
            _drawDeposit = False
            Dim depositAmount As String
            depositAmount = Game.GetUserInput("0", 65535)
            Dim [resume] = DateTime.UtcNow + TimeSpan.FromMilliseconds(200)
            Script.Wait(200)
            If Not String.IsNullOrEmpty(depositAmount) AndAlso depositAmount.IsNumberic() AndAlso Not depositAmount = "0" Then
                Dim amount As Long = Long.Parse(depositAmount)
                If amount >= Game.Player.Money Then amount = Game.Player.Money
                Select Case Game.Player.Character.Name
                    Case Game.GetGXTEntry("ACCNA_FRANKLIN")
                        fcBank += amount
                    Case Game.GetGXTEntry("ACCNA_MIKE")
                        mtBank += amount
                    Case Game.GetGXTEntry("ACCNA_TREVOR")
                        tpBank += amount
                End Select
                Game.Player.Money = (Game.Player.Money - amount - depFee)
                UpdateBank()
                _msgBoxText = Game.GetGXTEntry("MPATM_TRANCOM")
                _drawDeposit = False
                _drawMsgbox = True
            Else
                _msgBoxText = Game.GetGXTEntry("collision_9f1nje9")
                _drawDeposit = False
                _drawMsgbox = True
            End If
        ElseIf selectedButton Is btTrans Then
            _drawMenu = False
            Script.Wait(200)
            _drawTransfer = True
            hoveredButton = btFranklin
        ElseIf selectedButton Is btWithdraw Then
            Script.Wait(200)
            If Set_Amount = "NULL" Then
                RequestAdditionTextFile("FMMC")
                Set_Amount = Game.GetGXTEntry("collision_68lkszv")
            End If
            _drawMenu = False
            _drawWithdraw = True
            hoveredButton = btCustomw
        ElseIf selectedButton Is bt1kw Then
            WithdrawMoney(1000)
        ElseIf selectedButton Is bt5kw Then
            WithdrawMoney(5000)
        ElseIf selectedButton Is bt10kw Then
            WithdrawMoney(10000)
        ElseIf selectedButton Is bt50kw Then
            WithdrawMoney(50000)
        ElseIf selectedButton Is bt100kw Then
            WithdrawMoney(100000)
        ElseIf selectedButton Is bt500kw Then
            WithdrawMoney(500000)
        ElseIf selectedButton Is bt1mw Then
            WithdrawMoney(1000000)
        ElseIf selectedButton Is bt5mw Then
            WithdrawMoney(5000000)
        ElseIf selectedButton Is bt10mw Then
            WithdrawMoney(10000000)
        ElseIf selectedButton Is bt50mw Then
            WithdrawMoney(50000000)
        ElseIf selectedButton Is bt100mw Then
            WithdrawMoney(100000000)
        ElseIf selectedButton Is bt500mw Then
            WithdrawMoney(500000000)
        ElseIf selectedButton Is bt1bw Then
            WithdrawMoney(1000000000)
        ElseIf selectedButton Is btRemainw Then
            WithdrawMoney(GetWidhdrawRemainingAmount)
        ElseIf selectedButton Is btCustomw Then
            Try
                _drawWithdraw = False
                Dim withdrawAmount As String
                withdrawAmount = Game.GetUserInput("0", 65535)
                Script.Wait(200)
                If Not String.IsNullOrEmpty(withdrawAmount) AndAlso withdrawAmount.IsNumberic AndAlso Not withdrawAmount = "0" AndAlso withdrawAmount <= GetPlayerBankBalance() Then
                    Dim amount As Long = Long.Parse(withdrawAmount)
                    Select Case Game.Player.Character.Name
                        Case Game.GetGXTEntry("ACCNA_FRANKLIN")
                            If amount >= fcBank Then amount = fcBank
                            fcBank -= amount
                        Case Game.GetGXTEntry("ACCNA_MIKE")
                            If amount >= mtBank Then amount = mtBank
                            mtBank -= amount
                        Case Game.GetGXTEntry("ACCNA_TREVOR")
                            If amount >= tpBank Then amount = tpBank
                            tpBank -= amount
                    End Select
                    Game.Player.Money = (Game.Player.Money + amount - withFee)
                    UpdateBank()
                    _msgBoxText = Game.GetGXTEntry("MPATM_TRANCOM")
                    _drawWithdraw = False
                    _drawMsgbox = True
                Else
                    _msgBoxText = Game.GetGXTEntry("collision_9f1nje9")
                    _drawWithdraw = False
                    _drawMsgbox = True
                End If
            Catch ex As Exception
                _msgBoxText = Game.GetGXTEntry("collision_9f1nje9")
                _drawWithdraw = False
                _drawMsgbox = True
            End Try
        ElseIf selectedButton Is btFranklin Then
            If Not Game.Player.Character.Name = Game.GetGXTEntry("ACCNA_FRANKLIN") Then
                Script.Wait(200)
                _drawTransfer = False
                _drawTransfer2 = True
                hoveredButton = btCustomt
                selectedTrans = PlayerType.Franklin
                If Set_Amount = "NULL" Then
                    RequestAdditionTextFile("FMMC")
                    Set_Amount = Game.GetGXTEntry("collision_68lkszv")
                End If
            End If
        ElseIf selectedButton Is btMichael Then
            If Not Game.Player.Character.Name = Game.GetGXTEntry("ACCNA_MIKE") Then
                Script.Wait(200)
                _drawTransfer = False
                _drawTransfer2 = True
                hoveredButton = btCustomt
                selectedTrans = PlayerType.Michael
                If Set_Amount = "NULL" Then
                    RequestAdditionTextFile("FMMC")
                    Set_Amount = Game.GetGXTEntry("collision_68lkszv")
                End If
            End If
        ElseIf selectedButton Is btTrevor Then
            If Not Game.Player.Character.Name = Game.GetGXTEntry("ACCNA_TREVOR") Then
                Script.Wait(200)
                _drawTransfer = False
                _drawTransfer2 = True
                hoveredButton = btCustomt
                selectedTrans = PlayerType.Trevor
                If Set_Amount = "NULL" Then
                    RequestAdditionTextFile("FMMC")
                    Set_Amount = Game.GetGXTEntry("collision_68lkszv")
                End If
            End If
        ElseIf selectedButton Is bt1kt Then
            TransferMoney(1000, selectedTrans)
        ElseIf selectedButton Is bt5kt Then
            TransferMoney(5000, selectedTrans)
        ElseIf selectedButton Is bt10kt Then
            TransferMoney(10000, selectedTrans)
        ElseIf selectedButton Is bt50kt Then
            TransferMoney(50000, selectedTrans)
        ElseIf selectedButton Is bt100kt Then
            TransferMoney(100000, selectedTrans)
        ElseIf selectedButton Is bt500kt Then
            TransferMoney(500000, selectedTrans)
        ElseIf selectedButton Is bt1mt Then
            TransferMoney(1000000, selectedTrans)
        ElseIf selectedButton Is bt5mt Then
            TransferMoney(5000000, selectedTrans)
        ElseIf selectedButton Is bt10mt Then
            TransferMoney(10000000, selectedTrans)
        ElseIf selectedButton Is bt50mt Then
            TransferMoney(50000000, selectedTrans)
        ElseIf selectedButton Is bt100mt Then
            TransferMoney(100000000, selectedTrans)
        ElseIf selectedButton Is bt500mt Then
            TransferMoney(500000000, selectedTrans)
        ElseIf selectedButton Is bt1bt Then
            TransferMoney(1000000000, selectedTrans)
        ElseIf selectedButton Is btRemaint Then
            TransferMoney(GetWidhdrawRemainingAmount, selectedTrans)
        ElseIf selectedButton Is btCustomt Then
            _drawTransfer2 = False
            Dim transferAmount As String
            transferAmount = Game.GetUserInput("0", 65535)
            Script.Wait(200)
            If Not String.IsNullOrEmpty(transferAmount) AndAlso transferAmount.IsNumberic AndAlso Not transferAmount = "0" Then
                Dim amount As Long = Long.Parse(transferAmount)
                TransferMoney(amount, selectedTrans)
            End If
        End If
    End Sub

    Public Sub UpdateBank()
        config.SetValue(Of Long)("GENERAL", "Franklin", fcBank)
        config.SetValue(Of Long)("GENERAL", "Michael", mtBank)
        config.SetValue(Of Long)("GENERAL", "Trevor", tpBank)
        config.Save()
    End Sub

End Module
