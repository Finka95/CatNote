import React from 'react';
import Link from 'next/link';
import './HeaderWithMenu.scss';
import Button from '@mui/material/Button';
import ButtonGroup from '@mui/material/ButtonGroup';

export function HeaderWithMenu() {
    return (
        <div className='headerWithMenu'>
            <div className='headerWithMenu__name'>
                CatNote
            </div>

            <div className='headerWithMenu__buttonGroup'>
                <ButtonGroup variant="contained" aria-label="outlined primary button group">
                    <Link href="/users"><Button>Users</Button></Link>
                    <Link href="/tasks"><Button>Tasks</Button></Link>
                    <Link href="/achievements"><Button>Achievements</Button></Link>
                </ButtonGroup>
            </div>
        </div>
    );
}