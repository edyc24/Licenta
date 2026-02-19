package com.example.ajfilfov.controller;

import com.example.ajfilfov.entity.User;
import com.example.ajfilfov.entity.UserAddress;
import com.example.ajfilfov.service.UsersService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import java.util.*;

@RestController
@RequestMapping("/api/users")
public class UsersController {
    private final UsersService service;
    public UsersController(UsersService service) { this.service = service; }

    @GetMapping
    public List<User> getAllUsers() { return service.getAllUsers(); }

    @GetMapping("/{id}")
    public ResponseEntity<User> getUser(@PathVariable UUID id) {
        return service.getUserById(id).map(ResponseEntity::ok).orElse(ResponseEntity.notFound().build());
    }

    @PutMapping("/{id}")
    public ResponseEntity<User> update(@PathVariable UUID id, @RequestBody User user) {
        return ResponseEntity.ok(service.updateUser(id, user));
    }

    @GetMapping("/map")
    public List<UserAddress> getUserAddresses() { return service.getAllUserAddresses(); }
}
