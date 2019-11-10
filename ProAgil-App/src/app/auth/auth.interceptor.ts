import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpEvent, HttpHandler } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/internal/operators/tap';



Injectable({providedIn: 'root'});
export class Authinterceptor implements HttpInterceptor {

    /**
     *
     */
    constructor(private router: Router) {
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{
        if (localStorage.getItem('Token') != null) {
            const clonereq = req.clone({
                headers: req.headers.set('Authorization', `Bearer ${localStorage.getItem('Token')}`)
            });
            return next.handle(clonereq).pipe(
                tap(
                    succ => {},
                    error => {
                        if (error.status === 401) {
                            this.router.navigateByUrl('user/login');
                        }
                    }
                )
            );
        }
        else {
            return next.handle(req.clone());
        }
    }



}