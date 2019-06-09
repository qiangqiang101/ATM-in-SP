'Public Class Disabled

'    'If IsBrowsingWebsite("www_fleeca_com", 2) Then
'    '        'If _drawWebTransfer Then
'    '        '    DrawWebTransferOverlay(Color.FromArgb(232, 232, 232), Color.FromArgb(38, 143, 68), Color.LightGreen)
'    '        '    WebControlEventsTransfer(Color.FromArgb(38, 143, 68), Color.Gray)
'    '        '    WebButtonEvents()
'    '        '    ShowCursor()
'    '        'Else
'    '        '    DrawWebsiteOverlay(Color.FromArgb(232, 232, 232), Color.FromArgb(38, 143, 68), Color.LightGreen)
'    '        '    WebControlEvents(Color.FromArgb(38, 143, 68), Color.Gray)
'    '        '    WebButtonEvents()
'    '        '    ShowCursor()
'    '        'End If

'    '        ShowCursor()
'    '        Dim bgRect As New UIResRectangle(New Point(0, 138), UIMenu.GetScreenResolutionMaintainRatio.ToSize, Color.FromArgb(232, 232, 232)) :  bgRect.Draw()
'    '        Dim topRect As New UIResRectangle(New Point(0, 138), New Size(UIMenu.GetScreenResolutionMaintainRatio.ToSize.Width, 20), Color.FromArgb(38, 143, 68)) :  topRect.Draw()
'    '        Dim topSpr As New Sprite("commonmenu", "gradient_bgd", New Point(0, 138), New Size(UIMenu.GetScreenResolutionMaintainRatio.ToSize.Width, 20), 0F, Color.FromArgb(127, Color.FromArgb(38, 143, 68))) :  topSpr.Draw()

'    '        Dim grnRect As New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 450), Color.FromArgb(38, 143, 68)) :  grnRect.Draw()
'    '        Dim grnSpr As New Sprite("commonmenu", "gradient_bgd", New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 450), 0F, Color.FromArgb(127, Color.FromArgb(38, 143, 68))) :  grnSpr.Draw()
'    '        Dim balance As Long = 0
'    'Select Case Game.Player.Character.Name
'    'Case Game.GetGXTEntry("ACCNA_FRANKLIN")
'    '                balance = fcBank
'    '            Case Game.GetGXTEntry("ACCNA_MIKE")
'    '                balance = mtBank
'    '            Case Game.GetGXTEntry("ACCNA_TREVOR")
'    '                balance = tpBank
'    '            Case Else
'    '                balance = 0
'    '        End Select
'    'Dim lbIdNo As New UIResText($"{Game.GetGXTEntry("MPATM_ACBA")}:~n~${balance.ToString("###,###")}", New Point(grnRect.Position.X + (grnRect.Size.Width / 2), grnRect.Position.Y + 80), 0.5F, Color.White, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) :  lbIdNo.Draw()
'    '    End If








'    'Private Sub DrawWebsiteOverlay(bgColor As Color, titleColor As Color, btnColor As Color)
'    '    Dim bgRect As New UIResRectangle(New Point(0, 138), UIMenu.GetScreenResolutionMaintainRatio.ToSize, bgColor) : bgRect.Draw()
'    '    Dim topRect As New UIResRectangle(New Point(0, 138), New Size(UIMenu.GetScreenResolutionMaintainRatio.ToSize.Width, 20), titleColor) : topRect.Draw()
'    '    Dim topSpr As New Sprite("commonmenu", "gradient_bgd", New Point(0, 138), New Size(UIMenu.GetScreenResolutionMaintainRatio.ToSize.Width, 20), 0F, Color.FromArgb(127, titleColor)) : topSpr.Draw()


'    '    Dim redRect As New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 90), titleColor) : redRect.Draw()
'    '    Dim redSpr As New Sprite("commonmenu", "gradient_bgd", New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 90), 0F, Color.FromArgb(127, titleColor)) : redSpr.Draw()
'    '    Dim lbName As New UIResText($"{Game.Player.Character.Name}", New Point(redRect.Position.X + 5, redRect.Position.Y + 5), 0.5F, Color.White, GTA.Font.ChaletLondon, UIResText.Alignment.Left) : lbName.Draw()
'    '    Dim lbServ As New UIResText(Game.GetGXTEntry("MPATM_SER"), New Point(redRect.Position.X + (redRect.Size.Width / 2), redRect.Position.Y + 45), 0.5F, Color.White, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbServ.Draw()

'    '    btTransWeb.Draw()
'    '    Dim lbTrans As New UIResText(Game.GetGXTEntry("HUD_IMPORT").UppercaseFirstLetter, New Point(btTrans.Position.X + (btTrans.Size.Width / 2), btTrans.Position.Y + 30), 0.5F, Color.Black, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbTrans.Draw()
'    '    btDepositWeb.Draw()
'    '    Dim lbDeposit As New UIResText(Game.GetGXTEntry("MPATM_DIDM"), New Point(btDeposit.Position.X + (btDeposit.Size.Width / 2), btDeposit.Position.Y + 30), 0.5F, Color.Black, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbDeposit.Draw()
'    '    btWithdrawWeb.Draw()
'    '    Dim lbWithdraw As New UIResText(Game.GetGXTEntry("MPATM_WITM"), New Point(btWithdraw.Position.X + (btWithdraw.Size.Width / 2), btWithdraw.Position.Y + 30), 0.5F, Color.Black, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbWithdraw.Draw()
'    '    btBalanceWeb.Draw()
'    '    Dim lbBalance As New UIResText(Game.GetGXTEntry("W_BA_BAL"), New Point(btBalance.Position.X + (btBalance.Size.Width / 2), btBalance.Position.Y + 30), 0.5F, Color.Black, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbBalance.Draw()
'    'End Sub

'    'Private Sub DrawWebTransferOverlay(bgColor As Color, titleColor As Color, btnColor As Color)
'    '    Dim bgRect As New UIResRectangle(New Point(0, 138), UIMenu.GetScreenResolutionMaintainRatio.ToSize, bgColor) : bgRect.Draw()
'    '    Dim topRect As New UIResRectangle(New Point(0, 138), New Size(UIMenu.GetScreenResolutionMaintainRatio.ToSize.Width, 20), titleColor) : topRect.Draw()
'    '    Dim topSpr As New Sprite("commonmenu", "gradient_bgd", New Point(0, 138), New Size(UIMenu.GetScreenResolutionMaintainRatio.ToSize.Width, 20), 0F, Color.FromArgb(127, titleColor)) : topSpr.Draw()


'    '    Dim redRect As New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 90), titleColor) : redRect.Draw()
'    '    Dim redSpr As New Sprite("commonmenu", "gradient_bgd", New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 90), 0F, Color.FromArgb(127, titleColor)) : redSpr.Draw()
'    '    Dim lbName As New UIResText($"{Game.Player.Character.Name}", New Point(redRect.Position.X + 5, redRect.Position.Y + 5), 0.5F, Color.White, GTA.Font.ChaletLondon, UIResText.Alignment.Left) : lbName.Draw()
'    '    Dim lbServ As New UIResText(Game.GetGXTEntry("MPATM_SER"), New Point(redRect.Position.X + (redRect.Size.Width / 2), redRect.Position.Y + 45), 0.5F, Color.White, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbServ.Draw()

'    '    btMichaelWeb.Draw()
'    '    Dim lbMike As New UIResText(Game.GetGXTEntry("ACCNA_MIKE"), New Point(btMichael.Position.X + (btMichael.Size.Width / 2), btMichael.Position.Y + 30), 0.5F, Color.Black, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbMike.Draw()
'    '    btFranklinWeb.Draw()
'    '    Dim lbFrank As New UIResText(Game.GetGXTEntry("ACCNA_FRANKLIN"), New Point(btFranklin.Position.X + (btFranklin.Size.Width / 2), btFranklin.Position.Y + 30), 0.5F, Color.Black, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbFrank.Draw()
'    '    btTrevorWeb.Draw()
'    '    Dim lbTrev As New UIResText(Game.GetGXTEntry("ACCNA_TREVOR"), New Point(btTrevor.Position.X + (btTrevor.Size.Width / 2), btTrevor.Position.Y + 30), 0.5F, Color.Black, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbTrev.Draw()
'    'End Sub

'    'Private Sub WebControlEvents(hoverButtonColor As Color, normalButtonColor As Color)
'    '    For Each button As UIResRectangle In webButtonPool
'    '        If UIMenu.IsMouseInBounds(button.Position, button.Size) Then
'    '            button.Color = hoverButtonColor
'    '            If Game.IsControlPressed(0, GTA.Control.PhoneSelect) Then
'    '                selectedWebButton = button
'    '            Else
'    '                selectedWebButton = Nothing
'    '            End If
'    '        Else
'    '            button.Color = normalButtonColor
'    '        End If
'    '    Next
'    'End Sub

'    'Private Sub WebControlEventsTransfer(hoverButtonColor As Color, normalButtonColor As Color)
'    '    For Each button As UIResRectangle In webButtonPool2
'    '        If UIMenu.IsMouseInBounds(button.Position, button.Size) Then
'    '            button.Color = hoverButtonColor
'    '            If Game.IsControlPressed(0, GTA.Control.PhoneSelect) Then
'    '                selectedButton = button
'    '            Else
'    '                selectedButton = Nothing
'    '            End If
'    '        Else
'    '            button.Color = normalButtonColor
'    '        End If
'    '    Next
'    'End Sub

'    'Private Sub WebButtonEvents()
'    '    If selectedWebButton Is btBalanceWeb Then

'    '    ElseIf selectedWebButton Is btDepositWeb Then

'    '    ElseIf selectedWebButton Is btWithdrawWeb Then

'    '    ElseIf selectedWebButton Is btTransWeb Then
'    '        _drawWebTransfer = True
'    '    ElseIf selectedWebButton Is btFranklinWeb Then

'    '    ElseIf selectedWebButton Is btTrevorWeb Then

'    '    ElseIf selectedWebButton Is btMichaelWeb Then

'    '    End If
'    'End Sub

'End Class
