import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { RegisterCommand } from 'src/app/types/requests/RegisterCommand';
import { UsersService } from '../../services/api/users.service';
import { ValidationService } from '../../services/messenger/validation.service';
import { ErrorNotificationService } from '../../services/messenger/error-notification.service';
import { RoutingConstants } from '../../types/constants/RoutingConstants';
import { Subject, takeUntil } from 'rxjs';
import { TokensService } from 'src/app/services/messenger/tokens.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent implements OnDestroy {
  constructor(
    private _router: Router,
    private _usersService: UsersService,
    private _validationService: ValidationService,
    private _tokensService: TokensService,
    private _errorNotificationService: ErrorNotificationService
  ) {}

  public registerCommand: RegisterCommand = {
    username: '',
    password: ''
  };

  componentDestroyed$: Subject<boolean> = new Subject();

  public get routingConstants(): typeof RoutingConstants {
    return RoutingConstants;
  }

  ngOnDestroy(): void {
    this.componentDestroyed$.next(true);
    this.componentDestroyed$.complete();
  }

  onRegisterClick(): void {
    const emailFieldValidationResult = this._validationService.validateField(
      this.registerCommand.username,
      'Username'
    );

    const passwordFieldValidationResult = this._validationService.validateField(
      this.registerCommand.password,
      'Password'
    );

    if (!emailFieldValidationResult || !passwordFieldValidationResult) {
      return;
    }

    this._usersService
      .createUser(this.registerCommand)
      .pipe(takeUntil(this.componentDestroyed$))
      .subscribe({
        next: (response) => {
          this._tokensService.setTokens(response.tokens);
          this._router.navigateByUrl(this.routingConstants.Chats).then((r) => r);
        },
        error: (error) => {
          this._errorNotificationService.notifyOnError(error);
        }
      });
  }
}
