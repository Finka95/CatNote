import React from "react";
//import { useUser } from "@auth0/nextjs-auth0/dist/client";

export default function Home() {
  //const { user, error, isLoading } = useUser();
  return (
    <main>
      CatNote
      <a href="/api/auth/login">Login</a>
    </main>
  )
}
