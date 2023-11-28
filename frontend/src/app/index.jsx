import React from "react";
import { useUser } from "@auth0/nextjs-auth0/dist/client";

export default function index() {
    const { user, error, isLoading } = useUser();

    return <a href="/api/auth/login">Login</a>
}